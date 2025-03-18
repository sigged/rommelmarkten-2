
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Infrastructure.Security;
using Rommelmarkten.Api.Features.Users.Application.Security;
using Rommelmarkten.Api.Features.Users.Infrastructure.Security.AuthHandlers;

namespace Rommelmarkten.Api.Features.Users.Infrastructure.Security
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddUserAuthorization(this IServiceCollection services)
        {
            AuthorizationPolicies.AddAuthorizationPolicy(UsersPolicies.MustBeSelfOrAdmin, policy => policy.Requirements.Add(new MustBeSelfOrAdminRequirement()));

            services.AddSingleton<IAuthorizationHandler, MustBeSelfOrAdminAuthorizationHandler>();

            return services;
        }

    }

}
