using Microsoft.AspNetCore.Authorization;
using Rommelmarkten.Api.Common.Domain;
using System.Security.Claims;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace Rommelmarkten.Api.Common.Infrastructure.Security.AuthHandlers
{
    public class MustBeCreatorRequirement : IAuthorizationRequirement { }

    public class MustBeCreatorAuthorizationHandler : AuthorizationHandler<MustBeCreatorRequirement, IAuditable>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustBeCreatorRequirement requirement,
                                                       IAuditable resource)
        {
            if (context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.CreatedBy)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
