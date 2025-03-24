using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Rommelmarkten.ApiClient
{
    //public class AuthTokenService : ISecureTokenStore
    //{
    //    //private readonly AuthConfig _config;
    //    //private readonly SemaphoreSlim _lock = new(1, 1);
    //    //private string _cachedToken;
    //    //private DateTime _tokenExpiry = DateTime.MinValue;

    //    //public ClientSecretTokenService(IOptions<AuthConfig> config)
    //    //{
    //    //    _config = config.Value;
    //    //}

    //    public async Task<string> GetTokenAsync()
    //    {
    //        return string.Empty;
    //        //await _lock.WaitAsync();
    //        //try
    //        //{
    //        //    if (forceRefresh || string.IsNullOrEmpty(_cachedToken) || DateTime.UtcNow >= _tokenExpiry)
    //        //    {
    //        //        var newToken = await FetchNewTokenAsync();
    //        //        _cachedToken = newToken.AccessToken;
    //        //        _tokenExpiry = DateTime.UtcNow.AddSeconds(newToken.ExpiresIn * 0.75); // 75% of TTL
    //        //    }
    //        //    return _cachedToken;
    //        //}
    //        //finally
    //        //{
    //        //    _lock.Release();
    //        //}
    //    }

    //    //private async Task<TokenResponse> FetchNewTokenAsync()
    //    //{
    //    //    using var client = new HttpClient();
    //    //    var response = await client.PostAsync(_config.TokenEndpoint, new FormUrlEncodedContent(new[]
    //    //    {
    //    //        new KeyValuePair<string, string>("grant_type", "client_credentials"),
    //    //        new KeyValuePair<string, string>("client_id", _config.ClientId),
    //    //        new KeyValuePair<string, string>("client_secret", _config.ClientSecret)
    //    //    }));

    //    //    response.EnsureSuccessStatusCode();
    //    //    return await response.Content.ReadFromJsonAsync<TokenResponse>() ?? TokenResponse.Empty;
    //    //}
    //}
}
