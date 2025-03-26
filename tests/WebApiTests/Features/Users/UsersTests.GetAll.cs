using Rommelmarkten.ApiClient.Common;
using Rommelmarkten.EndToEndTests.WebApi.Common;
using System.Net;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Administrator)]
        public async Task GetAllUsers_AsAdmin_Returns200()
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);

            // Act
            var result = await client.Users.GetPaged(new PaginatedRequest(pageNumber: 1, pageSize: 10));

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetAllUsers_AsNonAdmin_Returns403()
        {
            // Arrange
            var client = appFixture.Client;
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: false);

            // Act
            var result = await client.Users.GetPaged(new PaginatedRequest(pageNumber: 1, pageSize: 10));

            // Assert
            Assert.False(result.Succeeded);
            Assert.NotNull(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetAllUsers_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();
            client.DefaultRequestHeaders.Clear();

            // Act
            var response = await client.GetAsync("/api/v1/Users");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

    }
}