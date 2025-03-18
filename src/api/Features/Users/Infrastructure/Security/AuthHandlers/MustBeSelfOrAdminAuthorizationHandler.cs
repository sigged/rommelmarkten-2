using Microsoft.AspNetCore.Authorization;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.Users.Domain;
using System.Security.Claims;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace Rommelmarkten.Api.Features.Users.Infrastructure.Security.AuthHandlers
{
    public class MustBeSelfOrAdminRequirement : IAuthorizationRequirement { }
    public class MustBeSelfOrAdminAuthorizationHandler : AuthorizationHandler<MustBeSelfOrAdminRequirement, UserProfile>
    {
        public override async Task HandleAsync(AuthorizationHandlerContext context)
        {
            if (!(context.Resource is UserProfile))
            {
                throw new InvalidOperationException($"Resource in {nameof(MustBeSelfOrAdminAuthorizationHandler)} must be of type {nameof(UserProfile)}");
            }
            else
            {
                await base.HandleAsync(context);
            }
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustBeSelfOrAdminRequirement requirement,
                                                       UserProfile resource)
        {
            if (context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.UserId ||
                context.User.HasClaim(c => c.Type == Common.Application.Security.ClaimTypes.IsAdmin))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
