using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.Markets.Domain;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Markets
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddMarketFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IEntityRepository<MarketConfiguration>, EFRepository<MarketConfiguration>>();
            services.AddScoped<IEntityRepository<MarketTheme>, EFRepository<MarketTheme>>();
            services.AddScoped<IEntityRepository<BannerType>, EFRepository<BannerType>>();

            return services;
        }
    }
}