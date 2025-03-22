using Microsoft.AspNetCore.Authorization;

namespace Rommelmarkten.Api.Common.Infrastructure.Security.AuthHandlers
{
    public class MustBeAdminRequirement : AuthorizationHandler<MustBeAdminRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        MustBeAdminRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == Application.Security.ClaimTypes.IsAdmin))
            {
                context.Succeed(requirement);
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
