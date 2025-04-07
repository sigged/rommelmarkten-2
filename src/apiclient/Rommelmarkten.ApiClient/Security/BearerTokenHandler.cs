using Rommelmarkten.ApiClient.Config;
using Rommelmarkten.ApiClient.Features.Users.ExchangeTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Rommelmarkten.ApiClient.Security
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private readonly ISecureTokenStore tokenService;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ApiClientConfiguration config;
        private readonly JwtSecurityTokenHandler jwtHandler;
        private readonly string[] noTokenEndpoints;

        public BearerTokenHandler(ISecureTokenStore tokenService, ApiClientConfiguration config, IHttpClientFactory httpClientFactory)
        {
            this.tokenService = tokenService;
            this.httpClientFactory = httpClientFactory;
            this.config = config;
            jwtHandler = new();

            noTokenEndpoints = [
                config.AuthConfig.RefreshTokenExchangeEndpoint
            ];
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if(!noTokenEndpoints.Any(ignored => 
                request?.RequestUri?.AbsolutePath?.EndsWith(ignored) ?? true))
            {
                await ProvideToken(request);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        private async Task ProvideToken(HttpRequestMessage request)
        {
            var accessToken = await tokenService.GetTokenAsync(TokenKeys.AccessToken);

            if(string.IsNullOrEmpty(accessToken))
            {
                accessToken = string.Empty;
            }

            switch (ValidateToken(accessToken))
            {
                case TokenValidationResult.Valid:
                    //no actions needed
                    break;

                case TokenValidationResult.Expired:
                    //try to exchange refresh token
                    accessToken = await ExchangeRefreshToken(accessToken!);
                    break;

                //case TokenValidationResult.InvalidIssuer:
                //case TokenValidationResult.InvalidAudience:
                //case TokenValidationResult.Malformed:
                //case TokenValidationResult.NotYetValid:
                default:
                    //unusable access token, clear store and provide empty one
                    await tokenService.ClearTokenAsync(TokenKeys.AccessToken);
                    await tokenService.ClearTokenAsync(TokenKeys.RefreshToken);
                    await tokenService.ClearTokenAsync(TokenKeys.DeviceHash);
                    accessToken = string.Empty;
                    break;
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        private async Task<string?> ExchangeRefreshToken(string currentAccessToken)
        {
            string newAccessToken = string.Empty;

            var refreshToken = await tokenService.GetTokenAsync(TokenKeys.RefreshToken);
            if(string.IsNullOrEmpty(refreshToken))
                return string.Empty;

            var deviceHash = await tokenService.GetTokenAsync(TokenKeys.DeviceHash) ?? string.Empty;

            // exchange token
            var client = httpClientFactory.CreateClient(Constants.ClientName);
            var response = await client.PostAsJsonAsync(config.AuthConfig.RefreshTokenExchangeEndpoint, new ExchangeTokenRequest
            {
                DeviceHash = deviceHash,
                OldTokenPair = new TokenPair
                {
                    AccessToken = currentAccessToken ?? string.Empty,
                    RefreshToken = refreshToken
                }
            });

            return newAccessToken;
        }

        private TokenValidationResult ValidateToken(string? token)
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
            if (jwt.Issuer != config.AuthConfig.ValidIssuer)
                return TokenValidationResult.InvalidIssuer;

            // Validate audience
            if (!jwt.Audiences.Contains(config.AuthConfig.ValidAudience))
                return TokenValidationResult.InvalidAudience;

            return TokenValidationResult.Valid;
        }

    }
}
