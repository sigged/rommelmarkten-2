using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.ApiClient.Config;
using Rommelmarkten.ApiClient.Extensions;
using Rommelmarkten.ApiClient.Security;
using Rommelmarkten.EndToEndTests.WebApi.Fakes;

namespace Rommelmarkten.EndToEndTests.WebApi
{
    public class Startup 
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ApiClientConfiguration
            {
                BaseUrl = "http://localhost/",
                TimeoutSeconds = 5,

                AuthConfig = new AuthConfig
                {
                    RefreshTokenExchangeEndpoint = "api/v1/Users/exchange-refresh-token",
                    ValidAudience = "rommelmarkten.be",
                    ValidIssuer = "api.rommelmarkten.be"
                }
            };

            services.AddSingleton<ISecureTokenStore, InMemoryTokenStore>();
            services.AddSingleton<TestClientHelper>();
            services.AddApiClient(config);
        }
    }
}
