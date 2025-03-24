using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;
using System.Reflection;

namespace Rommelmarkten.ApiClient
{
    public static class ApiClientExtensions
    {
        public static IHostApplicationBuilder AddApiClient(this IHostApplicationBuilder builder, ApiClientConfiguration config)
        {
            var version = AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location)
                                          .Version ?? new();

            string baseUrl = config.BaseUrl;
            if (!baseUrl.EndsWith('/'))
                baseUrl += '/';

            var httpBuilder = builder.Services.AddHttpClient(Constants.ClientName, (client) =>
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

            return builder;
        }

    }
}
