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
            await appFixture.TestHelper.Logout();
            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(GetEmailTokenByUrl_Unauthed_Returns401)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };
            var registerResult = await client.Users.Register(registerRequest);

            // Act
            var result = await client.Users.GetEmailConfirmationToken(registerResult.Data.RegisteredUserId);

            // Assert
            Assert.False(result?.Succeeded);
            Assert.Null(result?.Data);
            Assert.Equal(401, result?.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetEmailTokenByUrl_AsNonAdmin_Returns403()
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: false);
            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(GetEmailTokenByUrl_AsNonAdmin_Returns403)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };
            var registerResult = await client.Users.Register(registerRequest);

            // Act
            var result = await client.Users.GetEmailConfirmationToken(registerResult.Data.RegisteredUserId);

            // Assert
            Assert.False(result?.Succeeded);
            Assert.Null(result?.Data);
            Assert.Equal(403, result?.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Administrator)]
        public async Task GetEmailTokenByUrl_AsAdmin_Returns200()
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);
            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(GetEmailTokenByUrl_AsAdmin_Returns200)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };
            var registerResult = await client.Users.Register(registerRequest);

            // Act
            var result = await client.Users.GetEmailConfirmationToken(registerResult.Data.RegisteredUserId);

            // Assert
            Assert.True(result?.Succeeded);
            Assert.NotNull(result?.Data.Token);
        }


        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task ConfirmEmail_WithProperToken_Returns204()
        {
            // Arrange
            var client = appFixture.Client;
            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(ConfirmEmail_WithProperToken_Returns204)}",
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
            var registerRequest = new RegisterUserRequest
            {
                Name = "Mary Sommersville",
                Email = $"newuser@newuser.{nameof(ConfirmEmail_WithFaultyToken_Returns422)}",
                Password = "S3cure!",
                Captcha = "dummy"
            };
            var registerResult = await client.Users.Register(registerRequest);
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);
            var getTokenResult = await client.Users.GetEmailConfirmationToken(registerResult.Data.RegisteredUserId);

            var confirmCommand = new ConfirmEmailCommand
            {
                UserId = registerResult.Data.RegisteredUserId,
                ConfirmationToken = "faulty token"
            };

            // Act
            var result = await client.Users.ConfirmEmailToken(confirmCommand);

            // Assert
            Assert.False(result?.Succeeded);
            Assert.Equal(422, result?.Error.Status);
        }
    }
}