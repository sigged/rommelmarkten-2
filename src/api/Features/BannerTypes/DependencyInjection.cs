using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.FAQs.Domain;
using System.Reflection;

namespace Rommelmarkten.Api.Features.FAQs
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddFAQFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IEntityRepository<FAQCategory>, EFRepository<FAQCategory>>();
            services.AddScoped<IEntityRepository<FAQItem>, EFRepository<FAQItem>>();


            return services;
        }
    }
}