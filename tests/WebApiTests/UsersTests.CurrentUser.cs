using Rommelmarkten.FunctionalTests.WebApi.Common;
using Rommelmarkten.FunctionalTests.WebApi.Extensions;
using System.Net;

namespace WebApiTests.FunctionalTests
{
    public partial class UsersTests 
    {

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [Trait(Constants.Category, Constants.Trait_Enduser)]
        public async Task GetCurrentUser_Authed_Returns200(bool isAdminUser)
        {
            // Arrange
            var client = appFixture.Application.CreateClient();
            await client.Authenticate(isAdmin: isAdminUser);

            // Act
            var response = await client.GetAsync("/api/v1/Users/current");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        [Trait(Constants.Category, Constants.Trait_Enduser)]
        public async Task GetCurrentUser_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.Application.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/Users/current");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}