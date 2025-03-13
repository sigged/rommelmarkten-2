using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.Markets.Application.Gateways;
using Rommelmarkten.Api.Features.Markets.Domain;
using Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Markets
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddMarketFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IEntityRepository<MarketConfiguration>, EFRepository<MarketConfiguration, MarketsDbContext>>();
            services.AddScoped<IEntityRepository<MarketTheme>, EFRepository<MarketTheme, MarketsDbContext>>();
            services.AddScoped<IEntityRepository<BannerType>, EFRepository<BannerType, MarketsDbContext>>();

            services.AddScoped<IMarketsDbContext, MarketsDbContext>();
            services.AddDbContext<MarketsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}