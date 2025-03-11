
namespace Rommelmarkten.Api.Application.Common.Security
{
    public class AuthenticationTokenPair
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
