using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Solutaris.InfoWARE.ProtectedBrowserStorage.Extensions;

namespace Rommelmarkten.WebApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddIWProtectedBrowserStorageAsSingleton(); //optionally pass an encryption key (as string)


            await builder.Build().RunAsync();
        }
    }
}
