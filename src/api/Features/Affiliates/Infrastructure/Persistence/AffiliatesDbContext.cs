using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.Affiliates.Application.Gateways;
using Rommelmarkten.Api.Features.Affiliates.Domain;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Affiliates.Infrastructure.Persistence
{
    public class AffiliatesDbContext : DbContext, IAffiliatesDbContext
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IDomainEventService domainEventService;
        private readonly IDateTime dateTime;

        public required DbSet<AffiliateAd> AffiliateAds { get; set; }


        public AffiliatesDbContext(DbContextOptions<AffiliatesDbContext> options, ICurrentUserService currentUserService, IDomainEventService domainEventService, IDateTime dateTime)
            : base(options)
        {
            this.currentUserService = currentUserService;
            this.domainEventService = domainEventService;
            this.dateTime = dateTime;
        }

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
