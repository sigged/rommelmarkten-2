using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.ApiClient;
using Rommelmarkten.ApiClient.Features.Users;
using Rommelmarkten.ApiClient.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApiTests.EndToEndTests;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace Rommelmarkten.EndToEndTests.WebApi.Fakes
{
    public class TestClientHelper
    {
        private readonly ISecureTokenStore tokenStore;

        public TestClientHelper(ISecureTokenStore tokenStore)
        {
            this.tokenStore = tokenStore;
        }

        public async Task Authenticate(UsersClient client, bool isAdmin)
        {
            var loginRequest = new LoginRequest
            {
                Email = "thelma@localhost",
                Password = "Seedpassword1!"
            };
            if (isAdmin)
            {
                loginRequest = new LoginRequest
                {
                    Email = "administrator@localhost",
                    Password = "Seedpassword1!"
                };
            }
            var result = await client.Authenticate(loginRequest);
            if (result.Succeeded)
            {
                await tokenStore.StoreTokenAsync(TokenKeys.AccessToken, result.Data.AccessToken);
                await tokenStore.StoreTokenAsync(TokenKeys.RefreshToken, result.Data.RefreshToken);
            }
        }

        public async Task Authenticate(UsersClient client, string email, string password)
        {
            var loginRequest = new LoginRequest
            {
                Email = email,
                Password = password
            };

            var result = await client.Authenticate(loginRequest);
            if (result.Succeeded)
            {
                await tokenStore.StoreTokenAsync(TokenKeys.AccessToken, result.Data.AccessToken);
                await tokenStore.StoreTokenAsync(TokenKeys.RefreshToken, result.Data.RefreshToken);
            }
        }

        public async Task Logout()
        {
            await tokenStore.ClearTokenAsync(TokenKeys.AccessToken);
            await tokenStore.ClearTokenAsync(TokenKeys.RefreshToken);
            await tokenStore.ClearTokenAsync(TokenKeys.DeviceHash);
        }

        public async Task<string> RegisterUser(UsersClient client, string email, string password, bool confirmEmail = true)
        {
            var registerRequest = new RegisterUserRequest
            {
                Name = Guid.NewGuid().ToString(),
                Email = email,
                Password = password,
                Captcha = "dummy"
            };
            var registerResult = await client.Register(registerRequest);

            if (confirmEmail)
            {
                await Authenticate(client, isAdmin: true);
                var getTokenResult = await client.GetEmailConfirmationToken(registerResult.Data.RegisteredUserId);

                var confirmCommand = new ConfirmEmailCommand
                {
                    UserId = registerResult.Data.RegisteredUserId,
                    ConfirmationToken = getTokenResult.Data.Token
                };

                await client.ConfirmEmailToken(confirmCommand);
            }
            
            await Logout();

            return registerResult.Data.RegisteredUserId;
        }

        //private async Task<string> GetEmailConfirmationToken(UsersClient client, string userId)
        //{
        //    await client.(isAdmin: true);

        //    var result = await client.GetFromJsonAsync<TokenDto>($"/api/v1/Users/get-email-confirm-token?userId={userId}");

        //    client.Logout();

        //    return result?.Token ?? "";
        //}

    }
}
