using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Rommelmarkten.ApiClient
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly ISecureTokenStore tokenService;
        private readonly AuthConfig authConfig;
        private readonly JwtSecurityTokenHandler jwtHandler;

        public BearerTokenHandler(ISecureTokenStore tokenService, IOptions<AuthConfig> authConfig)
        {
            this.tokenService = tokenService;
            this.authConfig = authConfig.Value;
            this.jwtHandler = new();
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await tokenService.GetTokenAsync();

            switch (ValidateToken(token))
            {
                case TokenValidationResult.Valid:

                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    break;
                case TokenValidationResult.Expired:
                    token = "Refresh me";
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    break;
                case TokenValidationResult.InvalidIssuer:
                case TokenValidationResult.InvalidAudience:
                case TokenValidationResult.Malformed:
                case TokenValidationResult.NotYetValid:
                    token = string.Empty;
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    break;
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private TokenValidationResult ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return TokenValidationResult.Malformed;

            if (!jwtHandler.CanReadToken(token))
                return TokenValidationResult.Malformed;

            var jwt = jwtHandler.ReadJwtToken(token);
            var now = DateTime.UtcNow;

            // Validate expiration
            if (jwt.ValidTo < now)
                return TokenValidationResult.Expired;

            // Validate not-before time
            if (jwt.ValidFrom > now.AddMinutes(1))  // Allow 1 minute clock skew
                return TokenValidationResult.NotYetValid;

            // Validate issuer
            if (jwt.Issuer != authConfig.ValidIssuer)
                return TokenValidationResult.InvalidIssuer;

            // Validate audience
            if (!jwt.Audiences.Contains(authConfig.ValidAudience))
                return TokenValidationResult.InvalidAudience;

            return TokenValidationResult.Valid;
        }

    }
}
