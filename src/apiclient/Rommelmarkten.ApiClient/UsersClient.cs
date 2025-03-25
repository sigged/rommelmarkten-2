using Rommelmarkten.ApiClient.Config;
using System.Net.Http.Json;

namespace Rommelmarkten.ApiClient
{
    public class UsersClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ApiClientConfiguration configuration;

        public UsersClient(IHttpClientFactory httpClientFactory, ApiClientConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task<AuthenticationResult?> Authenticate(LoginRequest loginRequest)
        {
            var client = httpClientFactory.CreateClient(Constants.ClientName);
            var response = await client.PostAsJsonAsync("/api/v1/Users/login", loginRequest);
            var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>();
            return result;
        }


        public class LoginRequest
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }

        public class AuthenticationResult
        {
            public string? TokenType { get; set; }
            public required string AccessToken { get; set; }
            public required string RefreshToken { get; set; }
            public required int ExpiresIn { get; set; }
            public required bool Succeeded { get; set; }
            public required string[] Errors { get; set; } = [];

        }
    }
}
