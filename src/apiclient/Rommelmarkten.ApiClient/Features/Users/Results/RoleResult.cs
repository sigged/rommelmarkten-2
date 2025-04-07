namespace Rommelmarkten.ApiClient.Features.Users
{

    public partial class UsersClient
    {
        public class RoleResult
        {
            public required string Id { get; set; }
            public string? Name { get; set; }
        }
    }
}
