using Rommelmarkten.EndToEndTests.WebApi.Common;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_DeleteUser)]
        public async Task DeleteUser_AsAdmin_Returns204()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(DeleteUser_AsAdmin_Returns204)}",
                password: "S3cure!",
                confirmEmail: true);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);

            // Act
            var result = await client.Users.DeleteUser(userId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_DeleteUser)]
        public async Task DeleteUser_AsSelf_Returns204()
        {
            // Arrange
            var email = $"newuser@newuser.{nameof(DeleteUser_AsSelf_Returns204)}";
            var password = "S3cure!";
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email,
                password,
                confirmEmail: true);

            await appFixture.TestHelper.Logout();
            await appFixture.TestHelper.Authenticate(client.Users, email, password);

            var currentUser = await client.Users.GetCurrentUser();

            // Act
            var result = await client.Users.DeleteUser(currentUser.Data.Id!);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_DeleteUser)]
        public async Task DeleteUser_AsOtherUser_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(DeleteUser_AsOtherUser_Returns401)}",
                password: "S3cure!",
                confirmEmail: true);
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: false);

            // Act
            var result = await client.Users.DeleteUser(userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result?.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_DeleteUser)]
        public async Task DeleteUser_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(DeleteUser_Unauthed_Returns401)}",
                password: "S3cure!",
                confirmEmail: true);
            await appFixture.TestHelper.Logout();

            // Act
            var result = await client.Users.DeleteUser(userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result?.Error.Status);
        }
    }
}