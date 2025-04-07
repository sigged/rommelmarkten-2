namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        public class ChangeRoleCommand
        {
            public required string UserId { get; set; }
            public required string RoleId { get; set; }
        }
    }
}
