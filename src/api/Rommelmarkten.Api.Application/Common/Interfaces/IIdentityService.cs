using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Users;
using System.Security.Claims;

namespace Rommelmarkten.Api.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<IUser> FindByName(string userName);

        IQueryable<IUser> GetUsers();

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<Result> AuthenticateAsync(string userName, string password);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<IEnumerable<Claim>> GetClaims(IUser user);
        Task<Result> ConfirmEmailAsync(string userId, string token);
    }
}
