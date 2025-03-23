using Rommelmarkten.Api.Features.Users.Application.Commands.CreateUser;
using Rommelmarkten.FunctionalTests.WebApi.Common;
using Rommelmarkten.FunctionalTests.WebApi.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace WebApiTests.FunctionalTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(Constants.Category, Constants.Trait_Administrator)]
        public async Task RegisterNewUser_AsAdmin_Returns201()
        {
            // Arrange
            var client = appFixture.Application.CreateClient();
            await client.Authenticate(isAdmin: true);

            var command = new CreateUserCommand
            {
                Name = "Mary Sommersville 1",
                Email = "newuser1@newuser.com",
                Password = "S3cure!",
                Captcha = "dummy"
            };
            
            

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/Users/register", command);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        [Trait(Constants.Category, Constants.Trait_Enduser)]
        public async Task RegisterNewUser_AsEndUser_Returns201()
        {
            // Arrange
            var client = appFixture.Application.CreateClient();
            await client.Authenticate(isAdmin: false);

            var command = new CreateUserCommand
            {
                Name = "Mary Sommersville 2",
                Email = "newuser2@newuser.com",
                Password = "S3cure!",
                Captcha = "dummy"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/Users/register", command);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        [Trait(Constants.Category, Constants.Trait_Enduser)]
        public async Task RegisterNewUser_Unauthed_Returns201()
        {
            // Arrange
            var client = appFixture.Application.CreateClient();

            var command = new CreateUserCommand
            {
                Name = "Mary Sommersville 3",
                Email = "newuser3@newuser.com",
                Password = "S3cure!",
                Captcha = "dummy"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/Users/register", command);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}