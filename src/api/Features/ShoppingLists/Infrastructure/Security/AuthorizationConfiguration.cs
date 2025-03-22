
using Microsoft.AspNetCore.Authorization;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;
using System.Security.Claims;
using ClaimTypes = System.Security.Claims.ClaimTypes;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Security
{
    public static class AuthorizationConfiguration
    {
        //public static IServiceCollection AddShoppingListAuthorization(this IServiceCollection services)
        //{
        //    services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy(Policies.MustHaveListAccess, policy => policy.Requirements.Add(new MustHaveListAccessRequirement()));
        //        options.AddPolicy(Policies.MustMatchListAssociation, policy => policy.Requirements.Add(new MustMatchListAssociationRequirement()));
        //    });

        //    services.AddSingleton<IAuthorizationHandler, MustBeCreatorOrAdminAuthorizationHandler>();
        //    services.AddSingleton<IAuthorizationHandler, MustMatchListAssociationAuthorizationHandler>();

        //    return services;
        //}

    }

   
    public class MustHaveListAccessRequirement : AuthorizationHandler<MustHaveListAccessRequirement, ShoppingList>, IAuthorizationRequirement 
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

    public class MustMatchListAssociationRequirement : AuthorizationHandler<MustMatchListAssociationRequirement, Tuple<string, ShoppingList>>, IAuthorizationRequirement 
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
}
