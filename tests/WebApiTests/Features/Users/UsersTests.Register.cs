using Rommelmarkten.ApiClient.Common.Payloads;
using Rommelmarkten.EndToEndTests.WebApi.Common;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Administrator)]
        public async Task RegisterNewUser_AsAdmin_Returns201()
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);

            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(RegisterNewUser_AsAdmin_Returns201)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };

            // Act
            var result = await client.Users.Register(registerRequest);

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data.RegisteredUserId);
            Assert.Null(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task RegisterNewUser_AsEndUser_Returns201()
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: false);

            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(RegisterNewUser_AsEndUser_Returns201)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };

            // Act
            var result = await client.Users.Register(registerRequest);

            // Assert
            Assert.True(result?.Succeeded);
            Assert.NotNull(result?.Data.RegisteredUserId);
            Assert.Null(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task RegisterNewUser_Unauthed_Returns201()
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Logout();

            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(RegisterNewUser_Unauthed_Returns201)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };

            // Act
            var result = await client.Users.Register(registerRequest);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
            Assert.NotNull(result.Data.RegisteredUserId);
        }


        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task RegisterNewUser_InvalidEmail_Returns422()
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Logout();

            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = "",
                Password = "S3cure!",
                Captcha = "dummy"
            };

            // Act
            var result = await client.Users.Register(registerRequest);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(422, result.Error.Status);
            Assert.IsType<ValidationProblemDetails>(result?.Error);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task RegisterNewUser_InvalidName_Returns422()
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Logout();

            var registerRequest = new RegisterUserRequest
            {
                Name = "",
                Email = $"newuser@newuser.{nameof(RegisterNewUser_InvalidName_Returns422)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };

            // Act
            var result = await client.Users.Register(registerRequest);

            // Assert
            Assert.False(result?.Succeeded);
            Assert.Equal(422, result?.Error.Status);
            Assert.IsType<ValidationProblemDetails>(result?.Error);
        }
    }
}