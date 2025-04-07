namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        public class AuthenticationResult
        {
            public string? TokenType { get; set; }
            public required string AccessToken { get; set; }
            public required string RefreshToken { get; set; }
            public required int ExpiresIn { get; set; }
            public required bool Succeeded { get; set; }
            public required string[] Errors { get; set; } = [];

        }
    }
}
