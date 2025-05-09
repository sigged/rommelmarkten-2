﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.Affiliates.Application.Gateways;
using Rommelmarkten.Api.Features.Affiliates.Domain;
using Rommelmarkten.Api.Features.Affiliates.Infrastructure.Persistence;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Affiliates
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAffiliateFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IEntityRepository<AffiliateAd>, EFRepository<AffiliateAd, AffiliatesDbContext>>();

            services.AddScoped<IAffiliatesDbContext, AffiliatesDbContext>();
            services.AddDbContext<AffiliatesDbContext>(options => {
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