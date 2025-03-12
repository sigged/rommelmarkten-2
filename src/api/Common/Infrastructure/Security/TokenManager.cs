using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Common.Infrastructure.Identity;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Rommelmarkten.Api.Common.Infrastructure.Security
{

    public class TokenManager : ITokenManager
    {
        private readonly IIdentityService _identityService;
        private readonly TokenSettings tokenSettings;
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly ITokenValidationParametersFactory tokenValidationParmsFactory;
        private readonly IApplicationDbContext context;

        public TokenManager(
            IIdentityService identityService,
            TokenSettings tokenSettings,
            ITokenValidationParametersFactory tokenValidationParmsFactory,
            IApplicationDbContext context
        )
        {
            _identityService = identityService;
            //this.deviceIdGenerator = deviceIdGenerator;
            this.tokenSettings = tokenSettings;
            this.tokenValidationParmsFactory = tokenValidationParmsFactory;
            this.context = context;

            tokenHandler = new JwtSecurityTokenHandler();
        }


        public string GenerateRandomToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Base64UrlEncoder.Encode(randomNumber);
            }
        }


        public async Task<IPrincipal> GetAccessTokenPrincipal(string accessToken, bool ignoreExpiration = false)
        {
            var tokenValidationParms = tokenValidationParmsFactory.GetDefaultValidationParameters();
            tokenValidationParms.ValidateLifetime = !ignoreExpiration;
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParms, out var validatedToken); //todo: use async version
            return await Task.FromResult(principal);
        }

        public async Task<bool> IsValidRefreshToken(string refreshToken)
        {
            //var user = await _userManager.FindByNameAsync(username);

            var context = (ApplicationDbContext)this.context;

            return await context.Set<RefreshToken>().AnyAsync(e =>
                e.Token == refreshToken &&
                e.Expires > DateTime.UtcNow //expire date is in future
            );
        }

        public async Task RevokeToken(string refreshToken)
        {
            var context = (ApplicationDbContext)this.context;

            var token = await context.Set<RefreshToken>().FirstOrDefaultAsync(e =>
                   e.Token == refreshToken
               );

            if (token != null)
            {
                context.Set<RefreshToken>().Remove(token);
                await context.SaveChangesAsync();
            }
        }

        [Obsolete("Use GenerateAuthTokens instead")]
        public async Task<string> GenerateAuthTokenAsync(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = await _identityService.GetClaims(user);


            JwtSecurityToken accessToken;
            accessToken = CreateAccessToken(claims, tokenSettings);

            return tokenHandler.WriteToken(accessToken);
        }


        public async Task<AuthenticationTokenPair> GenerateAuthTokensAsync(IUser user, string deviceId)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = await _identityService.GetClaims(user);

            JwtSecurityToken accessToken;
            RefreshToken refreshToken;

            //generate new refresh token
            refreshToken = await CreateRefreshToken(user, deviceId, tokenSettings);
            //generate new access token
            accessToken = CreateAccessToken(claims, tokenSettings);

            return new AuthenticationTokenPair
            {
                AccessToken = tokenHandler.WriteToken(accessToken),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.Expires
            };
        }

        private async Task PurgeExpiredTokens()
        {
            var context = (ApplicationDbContext)this.context;

            var expiredRefreshTokens = await context.Set<RefreshToken>()
                .Where(e =>
                    e.Expires > DateTime.UtcNow //expire date is in future
                ).ToListAsync();

            context.Set<RefreshToken>().RemoveRange(expiredRefreshTokens);
            await context.SaveChangesAsync();
        }

        private async Task<RefreshToken> CreateRefreshToken(
            IUser user,
            string deviceId,
            TokenSettings tokenSettings)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var context = (ApplicationDbContext)this.context;

            //remove any expired tokens
            await PurgeExpiredTokens();

            string token = GenerateRandomToken();
            DateTime expiration = DateTime.UtcNow.AddMinutes(tokenSettings.RefreshTokenExpiryMinutes);

            var refreshToken = new RefreshToken(token, expiration, user.Id, deviceId);
            context.Set<RefreshToken>().Add(refreshToken);
            await context.SaveChangesAsync();

            return refreshToken;
        }


        private JwtSecurityToken CreateAccessToken(IEnumerable<Claim> claims, TokenSettings tokenSettings)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.ApiJwtKey));
            var signatureCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return new JwtSecurityToken(
                claims: claims,
                issuer: tokenSettings.JwtIssuer,
                audience: tokenSettings.JwtAudience,
                expires: DateTime.UtcNow.AddMinutes(tokenSettings.JwtExpiryMinutes),
                signingCredentials: signatureCredentials);
        }

    }

}
