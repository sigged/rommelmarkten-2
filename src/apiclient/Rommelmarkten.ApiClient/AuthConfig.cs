namespace Rommelmarkten.ApiClient
{
    public sealed class AuthConfig
    {
        public required Uri SomeTokenEndpoint { get; set; }
        public required string ValidIssuer { get; set; }
        public required string ValidAudience { get; set; }
    }
}
