using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Rommelmarkten.ApiClient.Config;
using Rommelmarkten.ApiClient.Extensions;
using Rommelmarkten.ApiClient.Security;
using Rommelmarkten.WebApp.Common;
using Solutaris.InfoWARE.ProtectedBrowserStorage.Extensions;

namespace Rommelmarkten.WebApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddApiClient(new ApiClientConfiguration
            {
                BaseUrl = "https://localhost:5001",
                TimeoutSeconds = 30,
                AuthConfig = new AuthConfig
                {
                    RefreshTokenExchangeEndpoint = "api/v1/Users/exchange-refresh-token",
                    ValidAudience = "",
                    ValidIssuer = ""
                }
            });
            builder.Services.AddTransient<ISecureTokenStore, BrowserTokenStore>();
            builder.Services.AddIWProtectedBrowserStorageAsSingleton(); //optionally pass an encryption key (as string)


            await builder.Build().RunAsync();
        }
    }
}
