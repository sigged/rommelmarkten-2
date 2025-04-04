using Rommelmarkten.EndToEndTests.WebApi.Common;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace WebApiTests.EndToEndTests
{
    public partial class UsersTests 
    {
        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_Profile)]
        public async Task UpdateProfile_AsAdmin_Returns204()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(UpdateProfile_AsAdmin_Returns204)}",
                password: "S3cure!",
                confirmEmail: true);

            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: true);

            var updateProfileCommand = new UpdateProfileCommand
            {
                UserId = userId,
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                Email = $"newuser@newuser.{nameof(UpdateProfile_AsAdmin_Returns204)}",
                HasConsented = true,
            };

            // Act
            var result = await client.Users.UpdateProfile(updateProfileCommand);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_Profile)]
        public async Task UpdateProfile_AsSelf_Returns204()
        {
            // Arrange
            var email = $"newuser@newuser.{nameof(UpdateProfile_AsSelf_Returns204)}";
            var password = "S3cure!";
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email,
                password,
                confirmEmail: true);

            await appFixture.TestHelper.Logout();
            await appFixture.TestHelper.Authenticate(client.Users, email, password);

            var currentUser = await client.Users.GetCurrentUser();

            var updateProfileCommand = new UpdateProfileCommand
            {
                UserId = currentUser.Data.Id!,
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                Email = $"newuser@newuser.{nameof(UpdateProfile_AsAdmin_Returns204)}",
                HasConsented = true,
            };

            // Act
            var result = await client.Users.UpdateProfile(updateProfileCommand);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_Profile)]
        public async Task UpdateProfile_WhenEmailChanges_InvalidateEmailAddress()
        {
            // Arrange
            var email = $"newuser@newuser.{nameof(UpdateProfile_WhenEmailChanges_InvalidateEmailAddress)}";
            var newEmail = $"changed@changed.{nameof(UpdateProfile_WhenEmailChanges_InvalidateEmailAddress)}";
            var password = "S3cure!";
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email,
                password,
                confirmEmail: true);

            await appFixture.TestHelper.Logout();
            await appFixture.TestHelper.Authenticate(client.Users, email, password);

            var currentUser = await client.Users.GetCurrentUser();

            var updateProfileCommand = new UpdateProfileCommand
            {
                UserId = currentUser.Data.Id!,
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                Email = newEmail,
                HasConsented = true,
            };


            // Act
            var result = await client.Users.UpdateProfile(updateProfileCommand);

            await appFixture.TestHelper.Logout();
            await appFixture.TestHelper.Authenticate(client.Users, newEmail, password);

            currentUser = await client.Users.GetCurrentUser();


            // Assert
            Assert.True(result.Succeeded);
            Assert.Null(result.Error);
            Assert.True(currentUser.Data.Email == newEmail);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_Profile)]
        public async Task UpdateProfile_AsOtherUser_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(UpdateProfile_AsOtherUser_Returns401)}",
                password: "S3cure!",
                confirmEmail: true);
            await appFixture.TestHelper.Authenticate(client.Users, isAdmin: false);

            var updateProfileCommand = new UpdateProfileCommand
            {
                UserId = userId,
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                Email = $"newuser@newuser.{nameof(UpdateProfile_AsAdmin_Returns204)}",
                HasConsented = true,
            };

            // Act
            var result = await client.Users.UpdateProfile(updateProfileCommand);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result?.Error.Status);
        }

        [Fact]
        [Trait(TestConstants.Trait_Users, TestConstants.Trait_Users_Profile)]
        public async Task UpdateProfile_Unauthed_Returns401()
        {
            // Arrange
            var client = appFixture.Client;
            var userId = await appFixture.TestHelper.RegisterUser(
                client.Users,
                email: $"newuser@newuser.{nameof(UpdateProfile_Unauthed_Returns401)}",
                password: "S3cure!",
                confirmEmail: true);
            await appFixture.TestHelper.Logout();

            var updateProfileCommand = new UpdateProfileCommand
            {
                UserId = userId,
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                Email = $"newuser@newuser.{nameof(UpdateProfile_AsAdmin_Returns204)}",
                HasConsented = true,
            };

            // Act
            var result = await client.Users.UpdateProfile(updateProfileCommand);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Null(result.Data);
            Assert.NotNull(result.Error);
            Assert.Equal(401, result?.Error.Status);
        }
    }
}