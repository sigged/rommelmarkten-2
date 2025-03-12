using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Rommelmarkten.Api.Features.ShoppingLists
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddShoppingListsFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));



            return services;
        }
    }
}