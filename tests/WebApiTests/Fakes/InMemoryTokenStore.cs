using Rommelmarkten.ApiClient.Security;

namespace Rommelmarkten.EndToEndTests.WebApi.Fakes
{
    public class InMemoryTokenStore : ISecureTokenStore
    {
        private readonly Dictionary<string, string> tokens = new Dictionary<string, string>();

        public Task ClearTokenAsync(string tokenKey)
        {
            if (tokens.ContainsKey(tokenKey))
            {
                tokens[tokenKey] = null!;
            }
            return Task.CompletedTask;
        }

        public Task<string?> GetTokenAsync(string tokenKey)
        {
            string? token = null;

            if (tokens.ContainsKey(tokenKey))
            {
                token = tokens[tokenKey];
            }
            else
            {
                token = null;
            }
            return Task.FromResult<string?>(token);
        }

        public Task StoreTokenAsync(string tokenKey, string token)
        {
            tokens[tokenKey] = token;
            return Task.CompletedTask;
        }
    }
}
