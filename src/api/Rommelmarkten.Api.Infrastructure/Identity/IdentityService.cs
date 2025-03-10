using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Exceptions;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Entities;
using Rommelmarkten.Api.Domain.Users;
using System.Security.Claims;

namespace Rommelmarkten.Api.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException(nameof(IUser), nameof(IUser.UserName));

            return user.UserName ?? string.Empty;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));
            
            return await _userManager.IsInRoleAsync(user, role);    
        }

        public async Task<string> CreateEmailConfirmationTokenAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException("User not found");

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<Result> ConfirmEmailAsync(string userId, string token)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                return Result.Failure(["Ongeldige confirmatiegegevens"]);

            var identityResult = await _userManager.ConfirmEmailAsync(user, token);
            if (identityResult.Succeeded)
            {
                return Result.Success();
            }
            else
            {
                return Result.Failure(identityResult.Errors.Select(e => e.Description));
            }
        }

        public async Task<Result> AuthenticateAsync(string userName, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, false, lockoutOnFailure: false);
            return result.ToApplicationResult();
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
                throw new NotFoundException(nameof(ApplicationUser), nameof(ApplicationUser.Id));

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<IUser> FindByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                throw new NotFoundException(nameof(IUser), nameof(IUser.UserName));

            return user;
        }

        public IQueryable<IUser> GetUsers()
        {
            return _userManager.Users;
        }

        public async Task<IEnumerable<Claim>> GetClaims(IUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var applicationUser = user as ApplicationUser;
            if (applicationUser == null)
                throw new ArgumentException("Parameter must be of type ApplicationUser", nameof(user));

            List<Claim> claimsForIdentity = new List<Claim>();

            // populate claims from assigned roles added to user
            foreach (var roleName in await _userManager.GetRolesAsync(applicationUser))
            {
                var role = await _roleManager.FindByNameAsync(roleName);

                if (role == null)
                    throw new NotFoundException(nameof(IdentityRole), nameof(IdentityRole.Name));

                claimsForIdentity.AddRange(await _roleManager.GetClaimsAsync(role));
            }
            
            // populate claims directly added to user
            claimsForIdentity.AddRange(await _userManager.GetClaimsAsync(applicationUser));

            // finally, add ID
            claimsForIdentity.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

            return claimsForIdentity;
        }
    }
}
