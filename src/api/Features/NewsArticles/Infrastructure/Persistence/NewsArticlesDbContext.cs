using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.NewsArticles.Application.Gateways;
using Rommelmarkten.Api.Features.NewsArticles.Domain;

namespace Rommelmarkten.Api.Features.NewsArticles.Infrastructure.Persistence
{
    public class NewsArticlesDbContext : ApplicationDbContextBase, INewsArticlesDbContext
    {
        public NewsArticlesDbContext(DbContextOptions<NewsArticlesDbContext> options, ICurrentUserService currentUserService, IDomainEventService domainEventService, IDateTime dateTime)
            : base(options, currentUserService, domainEventService, dateTime)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(NewsArticlesDbContext).Assembly);
        }

        public required DbSet<NewsArticle> NewsArticles { get; set; }

    }
}
