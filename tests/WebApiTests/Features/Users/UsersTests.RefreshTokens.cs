using Rommelmarkten.EndToEndTests.WebApi.Common;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_RefreshTokens)]
        public async Task ForceLogout_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(ForceLogout_Unauthed_Returns401)}",
                password: "S3cure!",
                confirmEmail: false);
            await appFixture.TestHelper.Logout();

            throw new NotImplementedException();
        }

    }
}