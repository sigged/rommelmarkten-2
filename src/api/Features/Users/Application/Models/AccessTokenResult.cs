using Rommelmarkten.Api.Common.Application.Models;

namespace Rommelmarkten.Api.Application.Users.Models
{
    public class AccessTokenResult : Result
    {
        public AccessTokenResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public string? TokenType { get; set; }

        public string? AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string? RefreshToken { get; set; }
    }
}
