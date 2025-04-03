using Moq;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.EndToEndTests.WebApi.Common;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {

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

    }
}