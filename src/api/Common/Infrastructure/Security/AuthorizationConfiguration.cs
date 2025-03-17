using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Domain;
using System.Security.Claims;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace Rommelmarkten.Api.Common.Infrastructure.Security
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(CorePolicies.MustBeAdmin, policy => policy.Requirements.Add(new MustBeAdminRequirement()));
                options.AddPolicy(CorePolicies.MustBeCreator, policy => policy.Requirements.Add(new MustBeCreatorRequirement()));
                options.AddPolicy(CorePolicies.MustBeCreatorOrAdmin, policy => policy.Requirements.Add(new MustBeCreatorOrAdminRequirement()));
                options.AddPolicy(CorePolicies.MustBeLastModifier, policy => policy.Requirements.Add(new MustBeLastModifierRequirement()));
            });

            services.AddTransient<IResourceAuthorizationService, ResourceAuthorizationService>();
            services.AddSingleton<IAuthorizationHandler, MustBeLastModifierAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MustBeAdminAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MustBeCreatorAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MustBeCreatorOrAdminAuthorizationHandler>();

            return services;
        }

    }

    public class MustBeAdminAuthorizationHandler : AuthorizationHandler<MustBeAdminRequirement, IAuditable>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustBeAdminRequirement requirement,
                                                       IAuditable resource)
        {
            if (context.User.HasClaim(c => c.Type == Common.Application.Security.ClaimTypes.IsAdmin))
            {
                context.Succeed(requirement);
            }

            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }

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
    public class MustBeCreatorOrAdminAuthorizationHandler : AuthorizationHandler<MustBeCreatorOrAdminRequirement, IAuditable>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustBeCreatorOrAdminRequirement requirement,
                                                       IAuditable resource)
        {
            if (context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.CreatedBy ||
                context.User.HasClaim(c => c.Type == Common.Application.Security.ClaimTypes.IsAdmin))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }


    public class MustBeLastModifierAuthorizationHandler : AuthorizationHandler<MustBeLastModifierRequirement, IAuditable>
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

    public class MustBeCreatorRequirement : IAuthorizationRequirement { }
    public class MustBeCreatorOrAdminRequirement : IAuthorizationRequirement { }
    public class MustBeLastModifierRequirement : IAuthorizationRequirement { }
    public class MustBeAdminRequirement : IAuthorizationRequirement { }
}
