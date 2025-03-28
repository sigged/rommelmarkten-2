using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Users.Domain;
using Rommelmarkten.ApiClient;
using Rommelmarkten.ApiClient.Security;
using Rommelmarkten.EndToEndTests.WebApi.Fakes;
using WebApiTests.EndToEndTests;

namespace Rommelmarkten.EndToEndTests.WebApi.Fixtures
{

    public class RommelmarktenWebApiFixture : IAsyncLifetime
    {
        private readonly IRommelmarktenClient client;
        private readonly TestClientHelper testHelper;
        private readonly BearerTokenHandler bearerTokenHandler;

        public required RommelmarktenWebApi RommelmarktenApi { get; set; }

        public IRommelmarktenClient Client => client;

        public TestClientHelper TestHelper => testHelper;

        public RommelmarktenWebApiFixture(IRommelmarktenClient client, TestClientHelper testClientHelper, BearerTokenHandler bearerTokenHandler)
        {
            this.client = client;
            this.testHelper = testClientHelper;
            this.bearerTokenHandler = bearerTokenHandler;
        }


        public Task InitializeAsync()
        {
            RommelmarktenApi = new RommelmarktenWebApi();

            client.SetHttpClientFactory(new InMemoryHttpClientFactory(RommelmarktenApi, [bearerTokenHandler]));

            // Seed the database with test data
            using var scope = RommelmarktenApi.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();
            var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

            return Task.CompletedTask;

            //// Create admin user
            //if (!userManager.Users.Any(u => u.UserName == "admin@example.com"))
            //{
            //    var adminUser = new ApplicationUser
            //    {
            //        UserName = "admin@example.com",
            //        Email = "admin@example.com",
            //        EmailConfirmed = true
            //    };
            //    await userManager.CreateAsync(adminUser, "TestPassword123!");
            //    await userManager.AddToRoleAsync(adminUser, "Admin");
            //}

            //// Create regular user
            //if (!userManager.Users.Any(u => u.UserName == "user@example.com"))
            //{
            //    var regularUser = new ApplicationUser
            //    {
            //        UserName = "user@example.com",
            //        Email = "user@example.com",
            //        EmailConfirmed = true
            //    };
            //    await userManager.CreateAsync(regularUser, "TestPassword123!");
            //    await userManager.AddToRoleAsync(regularUser, "User");
            //}

            //// Add other test data seeding as needed
        }

        public async Task DisposeAsync()
        {
            await RommelmarktenApi.DisposeAsync();
        }
    }
}