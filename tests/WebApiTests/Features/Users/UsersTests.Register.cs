using Rommelmarkten.Api.Features.Users.Application.Commands.CreateUser;
using Rommelmarkten.EndToEndTests.WebApi.Common;
using Rommelmarkten.EndToEndTests.WebApi.Extensions;
using System.Net;
using System.Net.Http.Json;
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
            Assert.True(result?.Succeeded);
            Assert.NotNull(result?.Data.RegisteredUserId);
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
            Assert.True(result?.Succeeded);
            Assert.NotNull(result?.Data.RegisteredUserId);
        }


        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task RegisterNewUser_InvalidEmail_Returns400()
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();
            var command = new CreateUserCommand
            {
                Name = "Mary Sommersville 4",
                Email = "",
                Password = "S3cure!",
                Captcha = "dummy"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/Users/register", command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task RegisterNewUser_InvalidName_Returns400()
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();

            var command = new CreateUserCommand
            {
                Name = "Mary Sommersville 4",
                Email = "",
                Password = "S3cure!",
                Captcha = "dummy"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/Users/register", command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}