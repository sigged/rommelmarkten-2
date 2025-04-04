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
            await appFixture.TestHelper.Logout();

            string newRoleId = null;

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

    }
}