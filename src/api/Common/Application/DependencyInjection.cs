using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Behaviours;
using System.Reflection;

namespace Rommelmarkten.Api.Common.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
                config.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
                config.AddOpenBehavior(typeof(ResourceAuthorizationBehaviour<,>));
                config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                config.AddOpenBehavior(typeof(CacheInvalidationBehaviour<,>));
                config.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
            });

            return services;
        }
    }
}
