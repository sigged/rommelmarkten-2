using Rommelmarkten.ApiClient.Security;
using Solutaris.InfoWARE.ProtectedBrowserStorage.Services;

namespace Rommelmarkten.WebApp.Common
{
    public class BrowserTokenStore : ISecureTokenStore
    {
        private readonly IIWLocalStorageService storage;

        public BrowserTokenStore(IIWLocalStorageService storage)
        {
            this.storage = storage;
        }

        public Task ClearTokenAsync(string tokenKey)
        {
            storage.RemoveItem(tokenKey);
            return Task.CompletedTask;
        }

        public Task<string?> GetTokenAsync(string tokenKey)
        {
            return storage.GetItemAsync<string?>(tokenKey);
        }

        public Task StoreTokenAsync(string tokenKey, string token)
        {
            return storage.SetItemAsync(tokenKey, token);
        }
    }
}
