using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Domain;
using System.Security.Claims;

namespace Rommelmarkten.Api.Common.Application.Interfaces
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
        Task<Result> ResetPasswordAsync(string email, string resetCode, string newPassword);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<IUser> FindByEmail(string email);
        //Task<string> GenerateRefreshTokenAsync(IUser user);
        Task<bool> IsLockedOutAsync(IUser user);
        Task<IUser> FindById(string id);
        Task<Result> UpdateUser(IUser user);
        Task<Result> InvalidateEmail(IUser user);
    }
}
