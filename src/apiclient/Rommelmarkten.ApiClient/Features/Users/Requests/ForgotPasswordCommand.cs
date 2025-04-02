namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        public class ForgotPasswordCommand
        {
            public required string Email { get; set; }

            public required string Captcha { get; set; }
        }
        
    }
}
