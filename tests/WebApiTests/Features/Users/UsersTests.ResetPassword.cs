using Moq;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.ApiClient.Common.Payloads;
using Rommelmarkten.EndToEndTests.WebApi.Common;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_ResetPassword)]
        public async Task GetPasswordResetTokenByUrl_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(GetPasswordResetTokenByUrl_Unauthed_Returns401)}",
                password: "S3cure!",
                confirmEmail: false);
            await appFixture.TestHelper.Logout();

            // Act
            var result = await client.Users.GetPasswordResetToken(userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_ResetPassword)]
        public async Task GetPasswordResetTokenByUrl_AsNonAdmin_Returns403()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(GetPasswordResetTokenByUrl_AsNonAdmin_Returns403)}",
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: false);

            // Act
            var result = await client.Users.GetPasswordResetToken(userId);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(403, result.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_ResetPassword)]
        public async Task GetPasswordResetTokenByUrl_AsAdmin_Returns200()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(GetPasswordResetTokenByUrl_AsAdmin_Returns200)}",
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);

            // Act
            var result = await client.Users.GetPasswordResetToken(userId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data.Token);
            Assert.Null(result.Error);
        }


        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_ResetPassword)]
        public async Task ForgotPassword_UnAuthed_Returns204()
        {
            // Arrange
            appFixture.RommelmarktenApi.Mocks.MailerMock.Reset();

            var email = $"newuser@newuser.{nameof(ForgotPassword_UnAuthed_Returns204)}";
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email,
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Logout();

            //Act
            var result = await client.Users.ForgotPassword(new ForgotPasswordCommand
            {
                Email = email,
                Captcha = "captcha"
            });

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
            appFixture.RommelmarktenApi.Mocks.MailerMock.Verify(m => m.SendPasswordResetLink(It.IsAny<IUser>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_ResetPassword)]
        public async Task ResetPassword_WithProperToken_Returns204()
        {
            // Arrange
            appFixture.RommelmarktenApi.Mocks.MailerMock.Reset();

            var email = $"newuser@newuser.{nameof(ResetPassword_WithProperToken_Returns204)}";
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email,
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);
            var getTokenResult = await client.Users.GetPasswordResetToken(userId);
            await appFixture.TestHelper.Logout();

            //Act
            var result = await client.Users.ResetPassword(new ResetPasswordCommand
            {
                Email = email,
                NewPassword = "S4cure!",
                ResetCode = getTokenResult.Data.Token,
            });

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_ResetPassword)]
        public async Task ResetPassword_WithFaultyToken_Returns222()
        {
            // Arrange
            var email = $"newuser@newuser.{nameof(ResetPassword_WithFaultyToken_Returns222)}";
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email,
                password: "S3cure!",
                confirmEmail: false);

            await appFixture.TestHelper.Logout();

            var confirmCommand = new ResetPasswordCommand
            {
                Email = email,
                NewPassword = "S4cure!",
                ResetCode = "faulty token"
            };

            // Act
            var result = await client.Users.ResetPassword(confirmCommand);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal(422, result.Error.Status);
            Assert.Null(result.Data);
            Assert.IsType<ValidationProblemDetails>(result?.Error);
        }
    }
}