namespace Rommelmarkten.ApiClient.Config
{
    public class ApiClientConfiguration
    {
        public required string BaseUrl { get; set; }
        public int TimeoutSeconds { get; set; }
        public required AuthConfig AuthConfig { get; set; }
    }

    public sealed class AuthConfig
    {
        public required string RefreshTokenExchangeEndpoint { get; set; }
        public required string ValidIssuer { get; set; }
        public required string ValidAudience { get; set; }
    }
}
