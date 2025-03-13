using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.FAQs.Application.Gateways;
using Rommelmarkten.Api.Features.FAQs.Domain;
using Rommelmarkten.Api.Features.FAQs.Infrastructure.Persistence;
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

            services.AddScoped<IFAQsDbContext, FAQsDbContext>();
            services.AddDbContext<FAQsDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}