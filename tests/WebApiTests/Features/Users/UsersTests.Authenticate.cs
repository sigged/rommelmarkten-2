﻿using Rommelmarkten.EndToEndTests.WebApi.Common;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_Authenticate)]
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
            Assert.NotNull(result?.Data.AccessToken);
            Assert.NotNull(result?.Data.RefreshToken);
            Assert.Null(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_Authenticate)]
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
            Assert.Null(result?.Data);
        }

    }
}