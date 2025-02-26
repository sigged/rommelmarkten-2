using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class MarketInvoiceReminderConfiguration : IEntityTypeConfiguration<MarketInvoiceReminder>
    {
        public void Configure(EntityTypeBuilder<MarketInvoiceReminder> builder)
        {
            builder.HasKey(e => e.Id);

        }
    }
}