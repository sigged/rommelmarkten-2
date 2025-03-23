using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Rommelmarkten.Api.WebApi;

namespace WebApiTests.FunctionalTests
{
    public class RommelmarktenWebApi : WebApplicationFactory<Program>
    {
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