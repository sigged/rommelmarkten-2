using Rommelmarkten.ApiClient.Features.Users;

namespace Rommelmarkten.ApiClient
{
    public interface IRommelmarktenClient
    {
        UsersClient Users { get; }

        void SetHttpClientFactory(IHttpClientFactory httpClientFactory);
    }
}
