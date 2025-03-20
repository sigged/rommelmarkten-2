using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Services;
using Rommelmarkten.Api.Features.NewsArticles.Application.Gateways;
using Rommelmarkten.Api.Features.NewsArticles.Domain;
using Rommelmarkten.Api.Features.NewsArticles.Infrastructure.Persistence;
using System.Reflection;

namespace Rommelmarkten.Api.Features.NewsArticles
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddNewsArticleFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IEntityRepository<NewsArticle>, EFRepository<NewsArticle, NewsArticlesDbContext>>();

            services.AddScoped<INewsArticlesDbContext, NewsArticlesDbContext>();
            services.AddDbContext<NewsArticlesDbContext>(options => {
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