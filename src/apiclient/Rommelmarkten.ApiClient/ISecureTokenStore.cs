namespace Rommelmarkten.ApiClient
{
    public interface ISecureTokenStore
    {
        Task<string?> GetTokenAsync();
        Task StoreTokenAsync(string token);
        Task ClearTokenAsync();
    }
}
