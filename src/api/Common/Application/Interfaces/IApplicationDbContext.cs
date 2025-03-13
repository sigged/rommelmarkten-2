using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Rommelmarkten.Api.Common.Application.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    {

        //DbSet<UserProfile> UserProfiles { get; set; }

        //DbSet<AffiliateAd> AffiliateAds { get; set; }

        //DbSet<NewsArticle> NewsArticles { get; set; }

        //DbSet<FAQCategory> FAQCategories { get; set; }

        //DbSet<FAQItem> FAQItems { get; set; }

        //DbSet<Province> Provinces { get; set; }

        //DbSet<BannerType> BannerTypes { get; set; }

        //DbSet<MarketConfiguration> MarketConfigurations { get; set; }

        //DbSet<MarketTheme> MarketThemes { get; set; }

        //DbSet<Market> Markets { get; set; }

        //DbSet<MarketDate> MarketDates { get; set; }

        //DbSet<MarketWithTheme> MarketWithThemes { get; set; }

        //DbSet<MarketInvoice> MarketInvoices { get; set; }

        //DbSet<MarketInvoiceLine> MarketInvoiceLines { get; set; }

        //DbSet<MarketInvoiceReminder> MarketInvoiceReminders { get; set; }

        //DbSet<MarketRevision> MarketRevisions { get; set; }

        //DbSet<MarketRevisionWithTheme> MarketRevisionsWithThemes { get; set; }



        //DbSet<ListItem> ListItems { get; set; }

        //DbSet<Category> Categories { get; set; }

        //DbSet<ShoppingList> ShoppingLists { get; set; }

        //DbSet<ListAssociate> ListAssociates { get; set; }

        ChangeTracker ChangeTracker { get; }

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

        Task<int> SaveChangesWithoutAutoAuditables(CancellationToken cancellationToken = new CancellationToken());
    }
}
