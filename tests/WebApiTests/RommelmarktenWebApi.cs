using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.WebApi;

namespace WebApiTests
{
    public class RommelmarktenWebApi : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

            builder.UseEnvironment("Testing");

            //builder.ConfigureAppConfiguration((context, config) =>
            //{
            //    var env = context.HostingEnvironment;
            //    config.AddJsonFile("appsettings.Testing.json", optional: false, reloadOnChange: true);
            //});

            builder.ConfigureTestServices(services =>
            {
                Environment.SetEnvironmentVariable("CacheSettings:UseCache", "false");

                var configuration = builder.UseConfiguration(new ConfigurationBuilder()
                    .AddJsonFile("testsettings.json")
                    .Build());

                // Replace real database with in-memory database for testing
                //var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                //if (descriptor != null)
                //    services.Remove(descriptor);


                //services.AddDbContext<ApplicationDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase("RommelmarktenInMemoryDb");
                //});

                // Additional service configurations or mocks can be added here
            });
        }
    }

}