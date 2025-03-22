using Microsoft.AspNetCore.Authorization;
using Rommelmarkten.Api.Common.Domain;
using System.Security.Claims;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace Rommelmarkten.Api.Common.Infrastructure.Security.AuthHandlers
{
    public class MustBeLastModifierRequirement : AuthorizationHandler<MustBeLastModifierRequirement, IAuditable>, IAuthorizationRequirement 
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                           MustBeLastModifierRequirement requirement,
                                                           IAuditable resource)
        {
            if (context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.LastModifiedBy)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

}
