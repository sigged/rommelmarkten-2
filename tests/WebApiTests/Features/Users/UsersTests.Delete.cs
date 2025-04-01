using Rommelmarkten.EndToEndTests.WebApi.Common;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Administrator)]
        public async Task DeleteUser_AsAdmin_Returns204()
        {
            // Arrange
            var client = appFixture.Client;
            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(DeleteUser_AsAdmin_Returns204)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };
            var registerResult = await client.Users.Register(registerRequest);
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);
            var getTokenResult = await client.Users.GetEmailConfirmationToken(registerResult.Data.RegisteredUserId);

            var confirmCommand = new ConfirmEmailCommand
            {
                UserId = registerResult.Data.RegisteredUserId,
                ConfirmationToken = getTokenResult.Data.Token
            };

            // Act
            var result = await client.Users.DeleteUser(registerResult.Data.RegisteredUserId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task DeleteUser_AsSelf_Returns204()
        {
            // Arrange
            var client = appFixture.Client;
            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(DeleteUser_AsAdmin_Returns204)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };
            var registerResult = await client.Users.Register(registerRequest);
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);
            var getTokenResult = await client.Users.GetEmailConfirmationToken(registerResult.Data.RegisteredUserId);

            var confirmCommand = new ConfirmEmailCommand
            {
                UserId = registerResult.Data.RegisteredUserId,
                ConfirmationToken = getTokenResult.Data.Token
            };
            await appFixture.TestHelper.Logout();

            await client.Users.ConfirmEmailToken(confirmCommand);
            await appFixture.TestHelper.Authenticate(client.Users, registerRequest.Email, registerRequest.Password);

            var currentUser = await client.Users.GetCurrentUser();

            // Act
            var result = await client.Users.DeleteUser(currentUser.Data.Id!);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task DeleteUser_AsOtherUser_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(DeleteUser_AsOtherUser_Returns401)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };
            var registerResult = await client.Users.Register(registerRequest);
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);
            var getTokenResult = await client.Users.GetEmailConfirmationToken(registerResult.Data.RegisteredUserId);

            var confirmCommand = new ConfirmEmailCommand
            {
                UserId = registerResult.Data.RegisteredUserId,
                ConfirmationToken = getTokenResult.Data.Token
            };

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: false);

            // Act
            var result = await client.Users.DeleteUser(registerResult.Data.RegisteredUserId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result?.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task DeleteUser_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(DeleteUser_Unauthed_Returns401)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };
            var registerResult = await client.Users.Register(registerRequest);

            await appFixture.TestHelper.Logout();

            // Act
            var result = await client.Users.DeleteUser(registerResult.Data.RegisteredUserId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result?.Error.Status);
        }
    }
}