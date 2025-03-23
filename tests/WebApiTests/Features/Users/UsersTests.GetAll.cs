using Rommelmarkten.Api.Common.Infrastructure.Security;
using Rommelmarkten.FunctionalTests.WebApi.Common;
using Rommelmarkten.FunctionalTests.WebApi.Extensions;
using Rommelmarkten.FunctionalTests.WebApi.Fixtures;
using System.Net;

namespace WebApiTests.FunctionalTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(Constants.Category, Constants.Trait_Administrator)]
        public async Task GetAllUsers_AsAdmin_Returns200()
        {
            // Arrange
            var client = appFixture.Application.CreateClient();
            await client.Authenticate(isAdmin: true);

            // Act
            var response = await client.GetAsync("/api/v1/Users");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        [Trait(Constants.Category, Constants.Trait_Enduser)]
        public async Task GetAllUsers_AsNonAdmin_Returns403()
        {
            // Arrange
            var client = appFixture.Application.CreateClient();
            await client.Authenticate(isAdmin: false);


            // Act
            var response = await client.GetAsync("/api/v1/Users");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        [Trait(Constants.Category, Constants.Trait_Enduser)]
        public async Task GetAllUsers_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.Application.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/Users");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

    }
}