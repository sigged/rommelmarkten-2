using Rommelmarkten.EndToEndTests.WebApi.Common;
using Rommelmarkten.EndToEndTests.WebApi.Extensions;
using System.Net;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetCurrentUser_Authed_Returns200(bool isAdminUser)
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();
            await client.Authenticate(isAdmin: isAdminUser);

            // Act
            var response = await client.GetAsync("/api/v1/Users/current");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetCurrentUser_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();
            client.DefaultRequestHeaders.Clear();

            // Act
            var response = await client.GetAsync("/api/v1/Users/current");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}