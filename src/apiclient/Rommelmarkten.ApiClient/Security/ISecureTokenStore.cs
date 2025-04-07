namespace Rommelmarkten.ApiClient.Security
{
    public interface ISecureTokenStore
    {
        Task<string?> GetTokenAsync(string tokenKey);
        Task StoreTokenAsync(string tokenKey, string token);
        Task ClearTokenAsync(string tokenKey);
    }
}
