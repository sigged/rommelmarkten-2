using Rommelmarkten.ApiClient;
using Solutaris.InfoWARE.ProtectedBrowserStorage.Services;

namespace Rommelmarkten.WebApp.Common
{
    public class BrowserTokenService : ISecureTokenStore
    {
        private readonly IIWLocalStorageService storage;
        private const string AccessTokenKey = "AccessToken";
        private const string RefreshTokenKey = "RefreshToken";

        public BrowserTokenService(IIWLocalStorageService storage)
        {
            this.storage = storage;
        }

        public Task ClearTokenAsync()
        {
            storage.RemoveItem(AccessTokenKey);
            return Task.CompletedTask;
        }

        public Task<string?> GetTokenAsync()
        {
            return storage.GetItemAsync<string?>(AccessTokenKey);
        }

        public Task StoreTokenAsync(string token)
        {
            return storage.SetItemAsync(AccessTokenKey, token);
        }
    }
}
