using Rommelmarkten.ApiClient.Common;
using Rommelmarkten.EndToEndTests.WebApi.Common;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_ChangeRole)]
        public async Task ChangeRole_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(ChangeRole_Unauthed_Returns401)}",
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);
            var roles = await client.Users.GetPagedRoles(new PaginatedRequest(1, 10));
            await appFixture.TestHelper.Logout();

            string newRoleId = roles.Data.Items.First().Id;

            // Act
            var result = await client.Users.ChangeRole(new ChangeRoleCommand
            {
                UserId = userId,
                RoleId = newRoleId
            });

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_ChangeRole)]
        public async Task ChangeRole_AsNonAdmin_Returns403()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(ChangeRole_AsNonAdmin_Returns403)}",
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);
            var roles = await client.Users.GetPagedRoles(new PaginatedRequest(1, 10));
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: false);
            string newRoleId = roles.Data.Items.First().Id;

            // Act
            var result = await client.Users.ChangeRole(new ChangeRoleCommand
            {
                UserId = userId,
                RoleId = newRoleId
            });

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(403, result.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_ChangeRole)]
        public async Task ChangeRole_AsAdmin_Returns204()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(ChangeRole_AsAdmin_Returns204)}",
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);
            var roles = await client.Users.GetPagedRoles(new PaginatedRequest(1, 10));

            string newRoleId = roles.Data.Items.First().Id;

            // Act
            var result = await client.Users.ChangeRole(new ChangeRoleCommand
            {
                UserId = userId,
                RoleId = newRoleId
            });

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
        }
    }
}