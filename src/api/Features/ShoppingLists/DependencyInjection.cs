using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Persistence;
using System.Reflection;

namespace Rommelmarkten.Api.Features.ShoppingLists
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddShoppingListsFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IShoppingListsDbContext, ShoppingListsDbContext>();
            services.AddDbContext<ShoppingListsDbContext>(options => {
                if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                {
                    options.UseInMemoryDatabase("RommelmarktenInMemoryDb");
                }
                else
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                }

            });

            return services;
        }
    }
}