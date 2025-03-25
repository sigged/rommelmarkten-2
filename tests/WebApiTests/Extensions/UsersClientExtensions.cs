using Rommelmarkten.ApiClient;
using Rommelmarkten.ApiClient.Features.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rommelmarkten.ApiClient.Features.Users.UsersClient;

namespace Rommelmarkten.EndToEndTests.WebApi.Extensions
{
    public static class UsersClientExtensions
    {

        public static async Task Authenticate(this UsersClient client, bool isAdmin)
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
               //todo: store token in fake provider...
            }
        }

        public static void Logout(this UsersClient client)
        {
            client.Logout();
        }
    }
}
