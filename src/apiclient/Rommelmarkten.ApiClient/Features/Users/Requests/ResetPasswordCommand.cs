namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        public class ResetPasswordCommand
        {
            public required string Email { get; set; }

            public required string ResetCode { get; set; }

            public required string NewPassword { get; set; }
        }
    }
}
