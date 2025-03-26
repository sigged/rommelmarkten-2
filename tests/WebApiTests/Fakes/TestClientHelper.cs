using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.ApiClient;
using Rommelmarkten.ApiClient.Features.Users;
using Rommelmarkten.ApiClient.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                await tokenStore.StoreTokenAsync(TokenKeys.AccessToken, result.AccessToken);
                await tokenStore.StoreTokenAsync(TokenKeys.RefreshToken, result.RefreshToken);
            }
        }

        public async Task Logout()
        {
            await tokenStore.ClearTokenAsync(TokenKeys.AccessToken);
            await tokenStore.ClearTokenAsync(TokenKeys.RefreshToken);
            await tokenStore.ClearTokenAsync(TokenKeys.DeviceHash);
            //client.Logout();
        }
    }

    //public static class UsersClientExtensions
    //{

    //    public static async Task Authenticate(this UsersClient client, bool isAdmin)
    //    {
    //        var loginRequest = new LoginRequest
    //        {
    //            Email = "thelma@localhost",
    //            Password = "Seedpassword1!"
    //        };

    //        if (isAdmin)
    //        {
    //            loginRequest = new LoginRequest
    //            {
    //                Email = "administrator@localhost",
    //                Password = "Seedpassword1!"
    //            };
    //        }

    //        var result = await client.Authenticate(loginRequest);

    //        if (result.Succeeded)
    //        {
    //           //todo: store token in fake provider...
    //        }
    //    }

    //    public static void Logout(this UsersClient client)
    //    {
    //        client.Logout();
    //    }
    //}
}
