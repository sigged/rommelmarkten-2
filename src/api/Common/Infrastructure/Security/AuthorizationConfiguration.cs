﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Infrastructure.Security.AuthHandlers;

namespace Rommelmarkten.Api.Common.Infrastructure.Security
{
    /// <summary>
    /// Holds future registrations of Authorization Policies
    /// </summary>
    public static class AuthorizationPolicies
    {
        public static Dictionary<string, Action<AuthorizationPolicyBuilder>> Policies { get; private set; } = new();

        public static void AddAuthorizationPolicy(string policyName, Action<AuthorizationPolicyBuilder> configurePolicy)
        {
            if (!Policies.ContainsKey(policyName))
            {
                Policies.Add(policyName, configurePolicy);
            }
            
        }

    }

    //public class AuthorizationPolicyContainer
    //{
    //    protected Dictionary<string, Action<AuthorizationPolicyBuilder>> policies { get; private set; } = new();

    //    public IReadOnlyDictionary<string, Action<AuthorizationPolicyBuilder>> Policies => policies;

    //    public void AddAuthorizationPolicy(string policyName, Action<AuthorizationPolicyBuilder> configurePolicy)
    //    {
    //        policies.Add(policyName, configurePolicy);
    //    }
    //}

    public static class AuthorizationConfiguration
    {
       
        public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
        {
            AuthorizationPolicies.AddAuthorizationPolicy(CorePolicies.MustBeAdmin, policy => policy.Requirements.Add(new MustBeAdminRequirement()));
            AuthorizationPolicies.AddAuthorizationPolicy(CorePolicies.MustBeCreator, policy => policy.Requirements.Add(new MustBeCreatorRequirement()));
            AuthorizationPolicies.AddAuthorizationPolicy(CorePolicies.MustBeCreatorOrAdmin, policy => policy.Requirements.Add(new MustBeCreatorOrAdminRequirement()));
            AuthorizationPolicies.AddAuthorizationPolicy(CorePolicies.MustBeLastModifier, policy => policy.Requirements.Add(new MustBeLastModifierRequirement()));

            services.AddAuthorization(options =>
            {
                foreach (var policyName in AuthorizationPolicies.Policies.Keys)
                {
                    options.AddPolicy(policyName, AuthorizationPolicies.Policies[policyName]);
                }
                AuthorizationPolicies.Policies.Clear();

            });

            services.AddTransient<IResourceAuthorizationService, ResourceAuthorizationService>();

            return services;
        }

    }

    
}
