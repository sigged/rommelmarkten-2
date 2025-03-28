namespace Rommelmarkten.ApiClient.Features.Users
{

    public partial class UsersClient
    {
        public class EmailConfirmTokenResult
        {
            public required string Token { get; set; }
        }
    }
}
