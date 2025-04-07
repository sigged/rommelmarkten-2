namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        public class ForceLogoutCommand
        {
            public required string UserId { get; set; }
        }
    }
}
