﻿using Microsoft.EntityFrameworkCore;
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
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IEntityRepository<FAQCategory>, EFRepository<FAQCategory, FAQsDbContext>>();
            services.AddScoped<IEntityRepository<FAQItem>, EFRepository<FAQItem, FAQsDbContext>>();

            services.AddScoped<IFAQsDbContext, FAQsDbContext>();
            services.AddDbContext<FAQsDbContext>(options => {
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