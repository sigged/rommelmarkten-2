
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.Users.Application.Security;
using System.Diagnostics;
using System.Security.Claims;

namespace Rommelmarkten.Api.Features.Users.Infrastructure.Security
{
    public static class AuthorizationConfiguration
    {
        //public static IServiceCollection AddUserAuthorization(this IServiceCollection services)
        //{
        //    services.AddAuthorization(options =>
        //    {
        //        options.AddPolicy(UsersPolicies.MustBeSelfOrAdmin, policy => policy.Requirements.Add(new MustBeSelfOrAdminRequirement()));
        //    });

        //    services.AddSingleton<IAuthorizationHandler, MustBeSelfOrAdminAuthorizationHandler>();

        //    return services;
        //}

    }

}
