using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.Affiliates.Domain;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Affiliates
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddAffiliateFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));



            services.AddScoped<IEntityRepository<AffiliateAd>, EFRepository<AffiliateAd>>();


            return services;
        }
    }
}