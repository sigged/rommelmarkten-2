using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence.Configuration
{
    public class MarketInvoiceReminderConfiguration : IEntityTypeConfiguration<MarketInvoiceReminder>
    {
        public void Configure(EntityTypeBuilder<MarketInvoiceReminder> builder)
        {
            builder.HasKey(e => e.Id);

        }
    }
}