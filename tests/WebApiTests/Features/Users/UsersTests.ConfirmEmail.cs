using Rommelmarkten.ApiClient.Common.Payloads;
using Rommelmarkten.EndToEndTests.WebApi.Common;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetEmailTokenByUrl_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(GetEmailTokenByUrl_Unauthed_Returns401)}",
                password: "S3cure!",
                confirmEmail: false);
            await appFixture.TestHelper.Logout();

            // Act
            var result = await client.Users.GetEmailConfirmationToken(userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetEmailTokenByUrl_AsNonAdmin_Returns403()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(GetEmailTokenByUrl_AsNonAdmin_Returns403)}",
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: false);

            // Act
            var result = await client.Users.GetEmailConfirmationToken(userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(403, result.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Administrator)]
        public async Task GetEmailTokenByUrl_AsAdmin_Returns200()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(GetEmailTokenByUrl_AsAdmin_Returns200)}",
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);

            // Act
            var result = await client.Users.GetEmailConfirmationToken(userId);

            // Assert
            Assert.True(result?.Succeeded);
            Assert.NotNull(result?.Data.Token);
            Assert.Null(result.Error);
        }


        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task ConfirmEmail_WithProperToken_Returns204()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(ConfirmEmail_WithProperToken_Returns204)}",
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);
            var getTokenResult = await client.Users.GetEmailConfirmationToken(userId);
            await appFixture.TestHelper.Logout();

            var confirmCommand = new ConfirmEmailCommand
            {
                UserId = userId,
                ConfirmationToken = getTokenResult.Data.Token
            };

            // Act
            var result = await client.Users.ConfirmEmailToken(confirmCommand);

            // Assert
            Assert.True(result?.Succeeded);
        }


        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task ConfirmEmail_WithFaultyToken_Returns422()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(ConfirmEmail_WithFaultyToken_Returns422)}",
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Logout();

            var confirmCommand = new ConfirmEmailCommand
            {
                UserId = userId,
                ConfirmationToken = "faulty token"
            };

            // Act
            var result = await client.Users.ConfirmEmailToken(confirmCommand);

            // Assert
            Assert.False(result?.Succeeded);
            Assert.Equal(422, result?.Error.Status);
            Assert.Null(result?.Data);
            Assert.IsType<ValidationProblemDetails>(result?.Error);
        }
    }
}