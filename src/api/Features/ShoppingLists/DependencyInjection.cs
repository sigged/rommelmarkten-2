using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Users
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddUsersFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));



            return services;
        }
    }
}