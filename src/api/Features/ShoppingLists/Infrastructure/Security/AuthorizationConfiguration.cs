
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Common.Infrastructure.Security;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Security;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;
using System.Security.Claims;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Security
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection AddShoppingListAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.MustHaveListAccess, policy => policy.Requirements.Add(new MustHaveListAccessRequirement()));
                options.AddPolicy(Policies.MustMatchListAssociation, policy => policy.Requirements.Add(new MustMatchListAssociationRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, MustBeCreatorOrAdminAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, MustMatchListAssociationAuthorizationHandler>();

            return services;
        }

    }

    public class MustHaveListAccessAuthorizationHandler : AuthorizationHandler<MustHaveListAccessRequirement, ShoppingList>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       MustHaveListAccessRequirement requirement,
                                                       ShoppingList resource)
        {
            if (context.User
                        .FindFirstValue(ClaimTypes.NameIdentifier) == resource.CreatedBy ||
                resource.Associates.Select(e => e.AssociateId)
                        .Contains(context.User.FindFirstValue(ClaimTypes.NameIdentifier)))
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

            if (context.User.FindFirstValue(ClaimTypes.NameIdentifier) == userIdToMatch &&
                shoppingList.Associates.Select(e => e.AssociateId)
                        .Contains(context.User.FindFirstValue(ClaimTypes.NameIdentifier)))
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

    public class MustHaveListAccessRequirement : IAuthorizationRequirement { }
    public class MustMatchListAssociationRequirement : IAuthorizationRequirement { }
}
