using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Users.Domain;
using WebApiTests.FunctionalTests;

namespace Rommelmarkten.FunctionalTests.WebApi.Fixtures
{
    public class RommelmarktenWebApiFixture : IAsyncLifetime
    {
        public RommelmarktenWebApi Application { get; private set; }

        public async Task InitializeAsync()
        {
            Application = new RommelmarktenWebApi();

            // Seed the database with test data
            using var scope = Application.Services.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<ApplicationDbContext>();
            var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

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
            await Application.DisposeAsync();
        }
    }
}