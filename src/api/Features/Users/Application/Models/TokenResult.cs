using Rommelmarkten.Api.Common.Application.Models;

namespace Rommelmarkten.Api.Features.Users.Application.Models
{
    public class TokenResult : Result
    {
        public TokenResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
        }

        public required string Token { get; set; }
    }

}
