using Rommelmarkten.Api.Common.Infrastructure.Security;
using Rommelmarkten.FunctionalTests.WebApi.Extensions;
using Rommelmarkten.FunctionalTests.WebApi.Fixtures;
using System.Net;

namespace WebApiTests.FunctionalTests
{
    [Collection(nameof(WebApiTests.FunctionalTests))]
    public class UsersTests : IClassFixture<RommelmarktenWebApiFixture>
    {
        private readonly RommelmarktenWebApiFixture appFixture;

        public UsersTests(RommelmarktenWebApiFixture fixture)
        {
            this.appFixture = fixture;
        }


        [Fact]
        public async Task GetAllUsers_AsAdmin_ShoudReturns200()
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
        public async Task GetAllUsers_AsNonAdmin_ShoudReturn401()
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
        public async Task GetAllUsers_Unauthed_ShoudReturn401()
        {
            // Arrange
            var client = appFixture.Application.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/Users");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Users_WhenGetCurrent_ShoudReturns200(bool isAdminUser)
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
    }
}