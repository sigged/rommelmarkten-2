namespace WebApiTests.FunctionalTests
{
    public partial class UsersTests 
    {

        [Fact]
        public async Task Authenticate_ValidCredentials_Returns200()
        {
            // Arrange
            var client = appFixture.Client;

            //arrange
            var result = await client.Users.Authenticate("administrator@localhost", "Seedpassword1!");

            // Assert
            Assert.True(result?.Succeeded);
        }

        [Fact]
        public async Task Authenticate_WithInvalidCredentials_Returns401()
        {
            // Arrange
            var client = appFixture.Client;

            // Act
            var result = await client.Users.Authenticate("administrator@localhost", "bad pass");

            // Assert
            Assert.False(result?.Succeeded);
        }
    }
}