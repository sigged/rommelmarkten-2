using static Rommelmarkten.ApiClient.UsersClient;

namespace WebApiTests.FunctionalTests
{
    public partial class UsersTests 
    {

        [Fact]
        public async Task Authenticate_ValidCredentials_Returns200()
        {
            // Arrange
            var client = appFixture.Client;
            var loginRequest = new LoginRequest
            {
                Email = "administrator@localhost",
                Password = "Seedpassword1!"
            };


            //arrange
            var result = await client.Users.Authenticate(loginRequest);

            // Assert
            Assert.True(result?.Succeeded);
        }

        [Fact]
        public async Task Authenticate_WithInvalidCredentials_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var loginRequest = new LoginRequest
            {
                Email = "administrator@localhost",
                Password = "bad pass"
            };

            // Act
            var result = await client.Users.Authenticate(loginRequest);

            // Assert
            Assert.False(result?.Succeeded);
        }
    }
}