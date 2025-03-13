using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;
using Rommelmarkten.Api.Features.Users.Infrastructure.Services;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Users
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddUsersFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IEntityRepository<UserProfile>, EFRepository<UserProfile, IUsersDbContext>>();
            services.AddTransient<IAvatarGenerator, AvatarGenerator>();

            services.AddScoped<IUsersDbContext, UsersDbContext>();
            services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            return services;
        }
    }
}