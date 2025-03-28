using Rommelmarkten.ApiClient.Common;
using Rommelmarkten.EndToEndTests.WebApi.Common;

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
            Assert.Equal(403, result.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Category, TestConstants.Trait_Enduser)]
        public async Task GetAllUsers_Unauthed_Returns401()
        {
            var client = appFixture.Client;
            await appFixture.TestHelper.Logout();

            // Act
            var result = await client.Users.GetPaged(new PaginatedRequest(pageNumber: 1, pageSize: 10));

            // Assert
            Assert.False(result.Succeeded);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result.Error.Status);
        }

    }
}