using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Rommelmarkten.Api.WebApi;

namespace WebApiTests.EndToEndTests
{
    public class RommelmarktenWebApi : WebApplicationFactory<Program>
    {
        private HttpClient client;

        public new HttpClient CreateClient()
        {
            return CreateClient(new WebApplicationFactoryClientOptions());
        }

        public new HttpClient CreateClient(WebApplicationFactoryClientOptions options)
        {
            if (client == null)
                client = base.CreateClient(options);
            return client;
        }
        

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            
            builder.UseEnvironment("Testing");

            builder.ConfigureTestServices(services =>
            {
                // Replace real database with in-memory database for testing
                //var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                //if (descriptor != null)
                //    services.Remove(descriptor);


                // Additional service configurations or mocks can be added here
            });
        }
    }
}