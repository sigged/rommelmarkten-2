using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.Affiliates.Application.Gateways;
using Rommelmarkten.Api.Features.Affiliates.Domain;
using Rommelmarkten.Api.Features.FAQs.Application.Gateways;
using Rommelmarkten.Api.Features.FAQs.Domain;
using Rommelmarkten.Api.Features.Markets.Application.Gateways;
using Rommelmarkten.Api.Features.Markets.Domain;
using Rommelmarkten.Api.Features.NewsArticles.Application.Gateways;
using Rommelmarkten.Api.Features.NewsArticles.Domain;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;
using Rommelmarkten.Api.Features.Users.Application.Gateways;
using Rommelmarkten.Api.Features.Users.Domain;
using Rommelmarkten.Api.Features.Users.Infrastructure.Persistence.Configuration;
using System.Reflection.Emit;

namespace Rommelmarkten.Api.MigrationsAggregator
{
    public class MigrationsDbContext : IdentityDbContext<
                                            ApplicationUser, ApplicationRole, string, 
                                            ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
                                            ApplicationRoleClaim, ApplicationUserToken>,
                                        IUsersDbContext, 
                                        IShoppingListsDbContext, 
                                        INewsArticlesDbContext, 
                                        IMarketsDbContext, 
                                        IFAQsDbContext, 
                                        IAffiliatesDbContext
    {
        private Type[] ContextTypes = [
            typeof(IUsersDbContext),
            typeof(IShoppingListsDbContext),
            typeof(INewsArticlesDbContext),
            typeof(IMarketsDbContext),
            typeof(IFAQsDbContext),
            typeof(IAffiliatesDbContext)
        ];

        public MigrationsDbContext(DbContextOptions<MigrationsDbContext> options) : base(options)
        {

        }

        public Task<int> SaveChangesWithoutAutoAuditables(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // configure base identity models first
            base.OnModelCreating(builder);

            // ensure navigation properties in custom models are mapped
            // warning! can't be done by using IEntityTypeConfiguration classes, so we use ModelBuilder directly
            builder.ConfigureApplicationIdentityModels();

            // now continue with all other configs
            foreach (var assembly in ContextTypes.Select(t => t.Assembly).DistinctBy(a => a.FullName))
            {
                builder.ApplyConfigurationsFromAssembly(assembly);
            }

            

        }

        public required DbSet<UserProfile> UserProfiles { get; set; }
        public required DbSet<ListItem> ListItems { get; set; }
        public required DbSet<Category> Categories { get; set; }
        public required DbSet<ShoppingList> ShoppingLists { get; set; }
        public required DbSet<ListAssociate> ListAssociates { get; set; }
        public required DbSet<NewsArticle> NewsArticles { get; set; }
        public required DbSet<BannerType> BannerTypes { get; set; }
        public required DbSet<Province> Provinces { get; set; }
        public required DbSet<MarketConfiguration> MarketConfigurations { get; set; }
        public required DbSet<MarketTheme> MarketThemes { get; set; }
        public required DbSet<Market> Markets { get; set; }
        public required DbSet<MarketDate> MarketDates { get; set; }
        public required DbSet<MarketWithTheme> MarketWithThemes { get; set; }
        public required DbSet<MarketInvoice> MarketInvoices { get; set; }
        public required DbSet<MarketInvoiceLine> MarketInvoiceLines { get; set; }
        public required DbSet<MarketInvoiceReminder> MarketInvoiceReminders { get; set; }
        public required DbSet<MarketRevision> MarketRevisions { get; set; }
        public required DbSet<MarketRevisionWithTheme> MarketRevisionsWithThemes { get; set; }
        public required DbSet<FAQCategory> FAQCategories { get; set; }
        public required DbSet<FAQItem> FAQItems { get; set; }
        public required DbSet<AffiliateAd> AffiliateAds { get; set; }
    }
}
