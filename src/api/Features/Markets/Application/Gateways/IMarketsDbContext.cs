using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.Gateways
{
    public interface IMarketsDbContext : IApplicationDbContext
    {
        DbSet<BannerType> BannerTypes { get; set;  }
        DbSet<Province> Provinces { get; set; }

        DbSet<MarketConfiguration> MarketConfigurations { get; set; }

        DbSet<MarketTheme> MarketThemes { get; set; }

        DbSet<Market> Markets { get; set; }

        DbSet<MarketDate> MarketDates { get; set; }

        DbSet<MarketWithTheme> MarketWithThemes { get; set; }

        DbSet<MarketInvoice> MarketInvoices { get; set; }

        DbSet<MarketInvoiceLine> MarketInvoiceLines { get; set; }

        DbSet<MarketInvoiceReminder> MarketInvoiceReminders { get; set; }

        DbSet<MarketRevision> MarketRevisions { get; set; }

        DbSet<MarketRevisionWithTheme> MarketRevisionsWithThemes { get; set; }

    }
}
