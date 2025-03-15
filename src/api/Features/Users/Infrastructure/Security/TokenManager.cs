using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Common.Infrastructure.Security;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Rommelmarkten.Api.Features.Users.Infrastructure.Security
{

    public class TokenManager : ITokenManager
    {
        private readonly IIdentityService _identityService;
        private readonly IPasswordHasher<ApplicationUser> passwordHasher;
        private readonly TokenSettings tokenSettings;
        private readonly JwtSecurityTokenHandler tokenHandler;
        private readonly ITokenValidationParametersFactory tokenValidationParmsFactory;
        private readonly IUsersDbContext context;

        public TokenManager(
            IIdentityService identityService,
            IPasswordHasher<ApplicationUser> passwordHasher,
            TokenSettings tokenSettings,
            ITokenValidationParametersFactory tokenValidationParmsFactory,
            IUsersDbContext context
        )
        {
            _identityService = identityService;
            this.passwordHasher = passwordHasher;
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

        public async Task<bool> IsValidRefreshToken(IUser user, string refreshToken, string deviceHash)
        {
            var appUser = (ApplicationUser)user;

            var refreshTokens = await context
                .Set<RefreshToken>()
                .Where(e => 
                    e.UserId == user.Id &&
                    e.DeviceHash == deviceHash &&
                    e.Expires > DateTime.UtcNow //expire date is in future
                )
                .ToListAsync();

            foreach (var storedToken in refreshTokens)
            {
                var validRefreshToken = passwordHasher.VerifyHashedPassword(appUser, storedToken.Token, refreshToken);

                if (validRefreshToken != PasswordVerificationResult.Failed)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task RevokeToken(string refreshToken)
        {
            var token = await context.Set<RefreshToken>().FirstOrDefaultAsync(e =>
                   e.Token == refreshToken
               );

            if (token != null)
            {
                context.Set<RefreshToken>().Remove(token);
                await context.SaveChangesAsync();
            }
        }

        //[Obsolete("Use GenerateAuthTokens instead")]
        //public async Task<string> GenerateAuthTokenAsync(IUser user)
        //{
        //    if (user == null)
        //        throw new ArgumentNullException(nameof(user));

        //    var claims = await _identityService.GetClaims(user);


        //    JwtSecurityToken accessToken;
        //    accessToken = CreateAccessToken(claims, tokenSettings);

        //    return tokenHandler.WriteToken(accessToken);
        //}


        public async Task<AuthenticationTokenPair> GenerateAuthTokensAsync(IUser user, string deviceHash)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var claims = (await _identityService.GetClaims(user)).ToList();
            claims.Add(new Claim(System.Security.Claims.ClaimTypes.Name, user.UserName!));

            JwtSecurityToken accessToken;
            RefreshToken refreshToken;

            //generate new refresh token
            refreshToken = await CreateRefreshToken(user, deviceHash, tokenSettings);
            //generate new access token
            accessToken = CreateAccessToken(claims, tokenSettings);

            return new AuthenticationTokenPair
            {
                AccessToken = tokenHandler.WriteToken(accessToken),
                RefreshToken = refreshToken.TokenRaw!,
                RefreshTokenExpiration = refreshToken.Expires
            };
        }

        private async Task PurgeExpiredTokens()
        {
            var context = this.context;

            var expiredRefreshTokens = await context.Set<RefreshToken>()
                .Where(e =>
                    e.Expires > DateTime.UtcNow //expire date is in future
                ).ToListAsync();

            context.Set<RefreshToken>().RemoveRange(expiredRefreshTokens);
            await context.SaveChangesAsync();
        }

        private async Task<RefreshToken> CreateRefreshToken(
            IUser user,
            string deviceHash,
            TokenSettings tokenSettings)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var appUser = (ApplicationUser)user;

            //remove any expired tokens
            await PurgeExpiredTokens();

            string token = GenerateRandomToken();
            string hashedToken = passwordHasher.HashPassword(appUser, token);

            DateTime expiration = DateTime.UtcNow.AddMinutes(tokenSettings.RefreshTokenExpiryMinutes);

            var refreshToken = new RefreshToken(hashedToken, expiration, appUser.Id, deviceHash);
            refreshToken.TokenRaw = token;

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
