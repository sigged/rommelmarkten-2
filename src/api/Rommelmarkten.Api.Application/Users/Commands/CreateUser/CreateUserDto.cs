using Rommelmarkten.Api.Application.Common.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rommelmarkten.Api.Application.Users.Queries.CreateUser;

public class CreateUserResult : Result
{
    public CreateUserResult(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
    {
        
    }

    public required string? RegisteredUserId { get; set; }

}
