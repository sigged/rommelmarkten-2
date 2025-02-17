using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Common;
using Rommelmarkten.Api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Security
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options => {
                options.AddPolicy(Policies.MustBeAdmin, policy => policy.RequireClaim(Application.Common.Security.ClaimTypes.IsAdmin));
                options.AddPolicy(Policies.MustBeCreator, policy => policy.Requirements.Add(new MustBeCreatorRequirement()));
                options.AddPolicy(Policies.MustBeLastModifier, policy => policy.Requirements.Add(new MustBeLastModifierRequirement()));
                options.AddPolicy(Policies.MustHaveListAccess, policy => policy.Requirements.Add(new MustHaveListAccessRequirement()));
                options.AddPolicy(Policies.MustMatchListAssociation, policy => policy.Requirements.Add(new MustMatchListAssociationRequirement()));
            });

            services.AddTransient<IResourceAuthorizationService, ResourceAuthorizationService>();
            services.AddSingleton<IAuthorizationHandler, MustBeLastModifierAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MustHaveListAccessAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MustBeCreatorAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MustMatchListAssociationAuthorizationHandler>();

            return services;
        }

    }

    public class MustBeAdminAuthorizationHandler : AuthorizationHandler<MustBeAdminRequirement, IAuditable>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustBeAdminRequirement requirement,
                                                       IAuditable resource)
        {
            if (context.User.HasClaim(c => c.Type == Application.Common.Security.ClaimTypes.IsAdmin))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class MustBeCreatorAuthorizationHandler : AuthorizationHandler<MustBeCreatorRequirement, IAuditable>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustBeCreatorRequirement requirement,
                                                       IAuditable resource)
        {
            if (context.User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier) == resource.CreatedBy)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class MustHaveListAccessAuthorizationHandler : AuthorizationHandler<MustHaveListAccessRequirement, ShoppingList>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustHaveListAccessRequirement requirement,
                                                       ShoppingList resource)
        {
            if ((context.User
                        .FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier) == resource.CreatedBy) ||
                resource.Associates.Select(e => e.AssociateId)
                        .Contains(context.User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier)))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class MustMatchListAssociationAuthorizationHandler : AuthorizationHandler<MustMatchListAssociationRequirement, Tuple<string, ShoppingList>>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustMatchListAssociationRequirement requirement,
                                                       Tuple<string, ShoppingList> resource)
        {
            string userIdToMatch = resource.Item1;
            var shoppingList = resource.Item2;

            if (context.User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier) == userIdToMatch &&
                shoppingList.Associates.Select(e => e.AssociateId)
                        .Contains(context.User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier)))
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
            if (context.User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier) == resource.LastModifiedBy)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

    public class MustBeCreatorRequirement : IAuthorizationRequirement { }
    public class MustBeLastModifierRequirement : IAuthorizationRequirement { }
    public class MustHaveListAccessRequirement : IAuthorizationRequirement { }
    public class MustBeAdminRequirement : IAuthorizationRequirement { }
    public class MustMatchListAssociationRequirement : IAuthorizationRequirement { }
}
