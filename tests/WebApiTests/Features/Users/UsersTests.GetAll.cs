using Rommelmarkten.ApiClient;
using Rommelmarkten.EndToEndTests.WebApi.Common;
using Rommelmarkten.EndToEndTests.WebApi.Extensions;
using System.Net;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Administrator)]
        public async Task GetAllUsers_AsAdmin_Returns200()
        {
            //// Arrange
            //var client = appFixture.RommelmarktenApi.CreateClient();
            //await client.Authenticate(isAdmin: true);

            //// Act
            //var response = await client.GetAsync("/api/v1/Users");
            //var content = await response.Content.ReadAsStringAsync();

            //// Assert
            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            // Arrange
            var client = appFixture.Client;
            await client.Users.Authenticate(isAdmin: true);

            // Act
            var users = await client.Users.GetPaged(new PaginatedRequest(pageNumber: 1, pageSize: 10));

            // Assert
            Assert.NotNull(users);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetAllUsers_AsNonAdmin_Returns403()
        {
            // Arrange
            var client = appFixture.RommelmarktenApi.CreateClient();
            await client.Authenticate(isAdmin: false);


            // Act
            var response = await client.GetAsync("/api/v1/Users");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
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