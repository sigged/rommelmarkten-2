using System.Text.Json.Serialization;

namespace Rommelmarkten.ApiClient
{
    // Token response DTO (matches standard OAuth 2.0 response format)
    public sealed class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public required string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public required int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public required string TokenType { get; set; }


        public static TokenResponse Empty => new TokenResponse
        {
            AccessToken = string.Empty,
            ExpiresIn = 0,
            TokenType = string.Empty
        };

    }
}
