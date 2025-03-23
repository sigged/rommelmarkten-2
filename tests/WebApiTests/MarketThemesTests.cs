using Rommelmarkten.FunctionalTests.WebApi.Extensions;
using Rommelmarkten.FunctionalTests.WebApi.Fixtures;
using System.Net;

namespace WebApiTests.FunctionalTests
{
    public class MarketThemesTests : IClassFixture<RommelmarktenWebApiFixture>
    {
        private readonly RommelmarktenWebApiFixture appFixture;

        public MarketThemesTests(RommelmarktenWebApiFixture fixture)
        {
            this.appFixture = fixture;
        }


        [Fact]
        public async Task ShoudReturns200_WhenGet()
        {
            // Arrange
            var client = appFixture.Application.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/MarketThemes");

            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Users_WhenGetAll_ShoudReturns200()
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