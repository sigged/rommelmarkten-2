using System.Net;

namespace WebApiTests
{
    public class MarketThemesTests
    {

        [Fact]
        public async Task ShoudReturns200_WhenGet()
        {
            // Arrange
            await using var application = new RommelmarktenWebApi();
            var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/MarketThemes");

            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Users_WhenGet_ShoudReturns200()
        {
            // Arrange
            await using var application = new RommelmarktenWebApi();
            var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/Users");

            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }

}