using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.ApiClient.Config;
using Rommelmarkten.ApiClient.Security;
using System.Net.Http.Headers;
using System.Reflection;

namespace Rommelmarkten.ApiClient.Extensions
{
    public static class ApiClientExtensions
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services, ApiClientConfiguration config)
        {
            var version = AssemblyName
                            .GetAssemblyName(Assembly.GetExecutingAssembly().Location)
                            .Version ?? new();

            string baseUrl = config.BaseUrl;
            if (!baseUrl.EndsWith('/'))
                baseUrl += '/';

            var httpBuilder = services.AddHttpClient(Constants.ClientName, (client) =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.Timeout = TimeSpan.FromSeconds(config.TimeoutSeconds);
                client.DefaultRequestHeaders.UserAgent.Clear();
                client.DefaultRequestHeaders.UserAgent.Add(
                    new ProductInfoHeaderValue(
                        new ProductHeaderValue(Constants.ClientAgentName, version.ToString()))
                );
            });


            httpBuilder.AddHttpMessageHandler<BearerTokenHandler>();

            services.AddSingleton<ApiClientConfiguration>(config);
            services.AddTransient<BearerTokenHandler>();

            services.AddTransient<IRommelmarktenClient>(provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                return new RommelmarktenClient(httpClientFactory, config);
            });

            return services;
        }

    }
}
