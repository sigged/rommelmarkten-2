using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Markets.Application.Gateways;
using Rommelmarkten.Api.Features.Markets.Domain;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence
{
    public class MarketsDbContext : DbContext, IMarketsDbContext
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IDomainEventService domainEventService;
        private readonly IDateTime dateTime;

        public MarketsDbContext(DbContextOptions<MarketsDbContext> options, ICurrentUserService currentUserService, IDomainEventService domainEventService, IDateTime dateTime)
            : base(options)
        {
            this.currentUserService = currentUserService;
            this.domainEventService = domainEventService;
            this.dateTime = dateTime;
        }

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



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<IAuditable> entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = currentUserService.UserId ?? string.Empty;
                        entry.Entity.Created = dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = currentUserService.UserId ?? string.Empty;
                        entry.Entity.LastModified = dateTime.Now;
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

        protected async Task DispatchEvents()
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
                await domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
