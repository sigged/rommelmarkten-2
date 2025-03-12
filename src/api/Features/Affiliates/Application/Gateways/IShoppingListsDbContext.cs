using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.Affiliates.Domain;

namespace Rommelmarkten.Api.Features.Affiliates.Application.Gateways
{
    public interface IAffiliatesDbContext : IApplicationDbContext
    {

        //DbSet<UserProfile> UserProfiles { get; set; }

        DbSet<AffiliateAd> AffiliateAds { get; set; }

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



    }
}
