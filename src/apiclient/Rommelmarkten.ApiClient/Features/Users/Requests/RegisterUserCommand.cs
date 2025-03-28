namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        public class RegisterUserRequest
        {
            public required string Name { get; set; }

            public required string Email { get; set; }

            public required string Password { get; set; }

            public required string Captcha { get; set; }
        }
    }
}
