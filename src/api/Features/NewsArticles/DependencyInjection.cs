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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<INewsArticlesDbContext, NewsArticlesDbContext>();

            services.AddScoped<IEntityRepository<NewsArticle>, EFRepository<NewsArticle>>();

            return services;
        }
    }
}