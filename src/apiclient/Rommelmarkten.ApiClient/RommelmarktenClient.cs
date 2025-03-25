using Rommelmarkten.ApiClient.Config;
using Rommelmarkten.ApiClient.Features.Users;

namespace Rommelmarkten.ApiClient
{
    public class RommelmarktenClient : IRommelmarktenClient
    {
        private IHttpClientFactory httpClientFactory;
        private readonly ApiClientConfiguration configuration;

        public RommelmarktenClient(IHttpClientFactory httpClientFactory, ApiClientConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;

            Users = new UsersClient(httpClientFactory, configuration);
        }

        public UsersClient Users { get; private set; }

        public void SetHttpClientFactory(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;

            Users = new UsersClient(httpClientFactory, configuration);
        }
    }
}
