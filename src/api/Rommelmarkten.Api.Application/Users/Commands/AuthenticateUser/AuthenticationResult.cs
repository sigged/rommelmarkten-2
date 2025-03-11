using Rommelmarkten.Api.Application.Common.Models;

namespace Rommelmarkten.Api.Application.Users.Commands.AuthenticateUser
{
    public class AuthenticationResult : Result
    {
        public AuthenticationResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {

        }

        public string? Token { get; set; }

        /*
            //response in identity       
            {
              "tokenType": "string",
              "accessToken": "string",
              "expiresIn": 0,
              "refreshToken": "string"
            }
         */
    }
}
