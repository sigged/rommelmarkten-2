namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        public class ResendConfirmationEmailCommand
        {
            public required string UserId { get; set; }
        }
    }
}
