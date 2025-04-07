namespace Rommelmarkten.ApiClient.Features.Users
{

    public partial class UsersClient
    {
        public class PasswordResetTokenResult
        {
            public required string Token { get; set; }
        }
    }
}
