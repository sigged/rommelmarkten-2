using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Domain.Affiliates;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Entities;
using Rommelmarkten.Api.Domain.Markets;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Application.Common.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {

        DbSet<MarketConfiguration> MarketConfigurations { get; set; }

        DbSet<MarketTheme> MarketThemes { get; set; }

        DbSet<BannerType> BannerTypes { get; set; }

        DbSet<AffiliateAd> AffiliateAds { get; set; }

        DbSet<NewsArticle> NewsArticles { get; set; }

        DbSet<FAQCategory> FAQCategories { get; set; }

        DbSet<FAQItem> FAQItems { get; set; }

        DbSet<ListItem> ListItems { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<ShoppingList> ShoppingLists { get; set; }

        DbSet<ListAssociate> ListAssociates { get; set; }

        DbSet<UserProfile> UserProfiles { get; set; }
        DbSet<MarketInvoice> MarketInvoices { get; set; }
        DbSet<MarketRevision> MarketRevisions { get; set; }
        DbSet<Market> Markets { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

        Task<int> SaveChangesWithoutAutoAuditables(CancellationToken cancellationToken = new CancellationToken());
    }
}
