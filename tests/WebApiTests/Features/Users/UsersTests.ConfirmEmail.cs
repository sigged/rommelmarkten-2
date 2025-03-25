using Rommelmarkten.Api.Features.Users.Application.Commands.ConfirmEmail;
using Rommelmarkten.Api.Features.Users.Application.Commands.CreateUser;
using Rommelmarkten.EndToEndTests.WebApi.Common;
using Rommelmarkten.EndToEndTests.WebApi.Extensions;
using System.Net;
using System.Net.Http.Json;

namespace WebApiTests.EndToEndTests
{
    public class CreatedUserResultDto
    {
        public string RegisteredUserId { get; set; }
        public bool Succeeded { get; set; }
    }
    public class TokenDto
    {
        public string Token { get; set; }
    }

    public partial class UsersTests 
    {
        private async Task<string> RegisterNewUser(HttpClient client, string email)
        {
            var command = new CreateUserCommand
            {
                Name = "Mary Sommersville 2",
                Email = email,
                Password = "S3cure!",
                Captcha = "dummy"
            };

            var response = await client.PostAsJsonAsync("/api/v1/Users/register", command);
            var result = await response.Content.ReadFromJsonAsync<CreatedUserResultDto>();

            return result?.RegisteredUserId ?? "";
        }

        private async Task<string> GetEmailConfirmationToken(HttpClient client, string userId)
        {
            await client.Authenticate(isAdmin: true);

            var result = await client.GetFromJsonAsync<TokenDto>($"/api/v1/Users/get-email-confirm-token?userId={userId}");

            client.Logout();

            return result?.Token ?? "";
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetEmailTokenByUrl_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();
            client.Logout();
            var newUserId = await RegisterNewUser(client, "getmy@token1");

            // Act
            var response = await client.GetAsync($"/api/v1/Users/get-email-confirm-token?userId={newUserId}");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetEmailTokenByUrl_AsNonAdmin_Returns403()
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();
            client.Logout();
            var newUserId = await RegisterNewUser(client, "getmy@token2");

            await client.Authenticate(isAdmin: false);

            // Act
            var response = await client.GetAsync($"/api/v1/Users/get-email-confirm-token?userId={newUserId}");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Administrator)]
        public async Task GetEmailTokenByUrl_AsAdmin_Returns200()
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();
            client.Logout();
            var newUserId = await RegisterNewUser(client, "getmy@token3");

            await client.Authenticate(isAdmin: true);

            // Act
            var response = await client.GetAsync($"/api/v1/Users/get-email-confirm-token?userId={newUserId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task ConfirmEmail_WithProperToken_Returns204()
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();
            client.Logout();
            var newUserId = await RegisterNewUser(client, "getmy@token4");
            var confirmationToken = await GetEmailConfirmationToken(client, newUserId);

            var command = new ConfirmEmailCommand
            {
                UserId = newUserId,
                ConfirmationToken = confirmationToken
            };

            // Act
            var response = await client.PostAsJsonAsync($"/api/v1/Users/confirm-email", command);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }


        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task ConfirmEmail_WithFaultyToken_Returns400()
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();
            client.Logout();
            var newUserId = await RegisterNewUser(client, "getmy@token5");
            var confirmationToken = "faulty";

            var command = new ConfirmEmailCommand
            {
                UserId = newUserId,
                ConfirmationToken = confirmationToken
            };

            // Act
            var response = await client.PostAsJsonAsync($"/api/v1/Users/confirm-email", command);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}