using Rommelmarkten.EndToEndTests.WebApi.Common;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_GetCurrentUser)]
        public async Task GetCurrentUser_Authed_Returns200(bool isAdminUser)
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: isAdminUser);

            // Act
            var result = await client.Users.GetCurrentUser();

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data);
            Assert.Null(result.Error);
        }


        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_GetCurrentUser)]
        public async Task GetCurrentUser_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Logout();

            // Act
            var result = await client.Users.GetCurrentUser();

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result.Error.Status);
        }
    }
}