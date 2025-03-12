using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.WebApi.Persistence.Configurations
{
    public class MarketInvoiceReminderConfiguration : IEntityTypeConfiguration<MarketInvoiceReminder>
    {
        public void Configure(EntityTypeBuilder<MarketInvoiceReminder> builder)
        {
            builder.HasKey(e => e.Id);

        }
    }
}