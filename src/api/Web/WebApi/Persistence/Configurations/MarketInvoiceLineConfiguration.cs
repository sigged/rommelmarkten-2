using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.WebApi.Persistence.Configurations
{
    public class MarketInvoiceLineConfiguration : IEntityTypeConfiguration<MarketInvoiceLine>
    {
        public void Configure(EntityTypeBuilder<MarketInvoiceLine> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(t => t.Subject)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Amount)
                .HasPrecision(18, 2);

        }
    }
}