using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;
using Rommelmarkten.Api.Features.Users.Infrastructure.Identity;
using Rommelmarkten.Api.Features.Users.Infrastructure.Security;
using Rommelmarkten.Api.Features.Users.Infrastructure.Services;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Users
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddUsersFeature(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IEntityRepository<UserProfile>, EFRepository<UserProfile, UsersDbContext>>();
            services.AddTransient<IAvatarGenerator, AvatarGenerator>();

            services.AddScoped<IUsersDbContext, UsersDbContext>();
            services.AddDbContext<UsersDbContext>(options => {
                if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                {
                    options.UseInMemoryDatabase("RommelmarktenInMemoryDb");
                }
                else
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                }

            });

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<ITokenManager, TokenManager>();

            services.AddUserAuthorization();

            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedEmail = true;
                    options.Stores.ProtectPersonalData = false; //todo: true!
                    options.Lockout.MaxFailedAccessAttempts = 5; //default
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //default
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<UsersDbContext>()
                .AddDefaultTokenProviders()
                .AddApiEndpoints();

            return services;
        }
    }
}