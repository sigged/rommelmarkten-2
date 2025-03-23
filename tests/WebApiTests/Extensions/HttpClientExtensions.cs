using Rommelmarkten.Api.Common.Application.Models;
using System.Net.Http.Json;

namespace Rommelmarkten.FunctionalTests.WebApi.Extensions
{
    public static class HttpClientExtensions
    {

        private class AuthResult
        {
            public required string AccessToken { get; set; }
        }

        public static void Logout(this HttpClient client)
        {
            client.DefaultRequestHeaders.Remove("Authorization");
        }

        public static async Task Authenticate(this HttpClient client, bool isAdmin)
        {
            var login = new
            {
                Email = "thelma@localhost",
                Password = "Seedpassword1!"
            };

            if (isAdmin)
            {
                login = new
                {
                    Email = "administrator@localhost",
                    Password = "Seedpassword1!"
                };
            }

            var response = await client.PostAsJsonAsync("/api/v1/Users/login", login);
            if(response.IsSuccessStatusCode)
            {

                var result = await response.Content.ReadFromJsonAsync<AuthResult>();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {result?.AccessToken}");
            }
            else
            {
                throw new ApplicationException("Failed to authenticate as admin");
            }
        }
    }
}
