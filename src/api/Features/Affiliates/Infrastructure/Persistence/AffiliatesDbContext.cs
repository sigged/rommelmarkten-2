using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Affiliates.Application.Gateways;
using Rommelmarkten.Api.Features.Affiliates.Domain;
using System.Reflection;

namespace Rommelmarkten.Api.Features.Affiliates.Infrastructure.Persistence
{
    public class AffiliatesDbContext : ApplicationDbContextBase, IAffiliatesDbContext
    {
        public AffiliatesDbContext(
            DbContextOptions<AffiliatesDbContext> options,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime
            )
                : base(options, currentUserService, domainEventService, dateTime)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }


        public required DbSet<AffiliateAd> AffiliateAds { get; set; }

    }
}
