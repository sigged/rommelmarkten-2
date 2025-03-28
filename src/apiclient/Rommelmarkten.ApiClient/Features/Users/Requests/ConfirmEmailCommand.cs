namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        public class ConfirmEmailCommand
        {
            public required string UserId { get; set; }

            public required string ConfirmationToken { get; set; }
        }
    }
}
