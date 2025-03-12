using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.Affiliates.Domain;
using Rommelmarkten.Api.Features.FAQs.Domain;
using Rommelmarkten.Api.Features.Markets.Domain;
using Rommelmarkten.Api.Features.NewsArticles.Domain;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;
using Rommelmarkten.Api.Features.Users.Domain;
using Rommelmarkten.Api.Features.Users.Infrastructure.Identity;
using System.Reflection;

namespace Rommelmarkten.Api.WebApi.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions options,
            //IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public required DbSet<UserProfile> UserProfiles { get; set; }

        public required DbSet<AffiliateAd> AffiliateAds { get; set; }

        public required DbSet<NewsArticle> NewsArticles { get; set; }

        public required DbSet<FAQCategory> FAQCategories { get; set; }

        public required DbSet<FAQItem> FAQItems { get; set; }

        public required DbSet<Province> Provinces { get; set; }

        public required DbSet<BannerType> BannerTypes { get; set; }

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



        public required DbSet<ListItem> ListItems { get; set; }

        public required DbSet<Category> Categories { get; set; }

        public required DbSet<ShoppingList> ShoppingLists { get; set; }

        public required DbSet<ListAssociate> ListAssociates { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<IAuditable> entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId ?? string.Empty;
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId ?? string.Empty;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        public async Task<int> SaveChangesWithoutAutoAuditables(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
