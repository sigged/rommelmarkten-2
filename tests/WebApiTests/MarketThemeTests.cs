using Rommelmarkten.FunctionalTests.WebApi.Fixtures;
using System.Net;

namespace WebApiTests.FunctionalTests
{
    [Collection(nameof(FunctionalTests))]
    public class MarketThemeTests : IClassFixture<RommelmarktenWebApiFixture>
    {
        private readonly RommelmarktenWebApiFixture appFixture;

        public MarketThemeTests(RommelmarktenWebApiFixture fixture)
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

    }
}