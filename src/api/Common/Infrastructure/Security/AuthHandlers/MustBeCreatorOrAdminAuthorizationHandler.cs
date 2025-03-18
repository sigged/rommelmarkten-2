using Microsoft.AspNetCore.Authorization;
using Rommelmarkten.Api.Common.Domain;
using System.Security.Claims;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace Rommelmarkten.Api.Common.Infrastructure.Security.AuthHandlers
{
    public class MustBeCreatorOrAdminRequirement : IAuthorizationRequirement { }

    public class MustBeCreatorOrAdminAuthorizationHandler : AuthorizationHandler<MustBeCreatorOrAdminRequirement, IAuditable>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustBeCreatorOrAdminRequirement requirement,
                                                       IAuditable resource)
        {
            if (context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.CreatedBy ||
                context.User.HasClaim(c => c.Type == Application.Security.ClaimTypes.IsAdmin))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
