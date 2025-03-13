using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.Markets.Application.Gateways;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence
{
    public class MarketsDbContext : ApplicationDbContextBase, IMarketsDbContext
    {
        public MarketsDbContext(DbContextOptions<MarketsDbContext> options, ICurrentUserService currentUserService, IDomainEventService domainEventService, IDateTime dateTime)
            : base(options, currentUserService, domainEventService, dateTime)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(MarketsDbContext).Assembly);
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

    }
}
