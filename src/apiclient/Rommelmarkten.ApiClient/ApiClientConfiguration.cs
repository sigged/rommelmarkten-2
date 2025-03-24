namespace Rommelmarkten.ApiClient
{
    public class ApiClientConfiguration
    {
        public required string BaseUrl { get; set; }
        public int TimeoutSeconds { get; set; }
    }
}
