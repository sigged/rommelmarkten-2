using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Infrastructure.Persistence.Configuration
{
    public class MarketInvoiceConfiguration : IEntityTypeConfiguration<MarketInvoice>
    {
        public void Configure(EntityTypeBuilder<MarketInvoice> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(e => e.InvoiceNumber)
                .IsUnique();

            builder.HasMany(e => e.InvoiceLines)
                .WithOne(e => e.ParentInvoice)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.PaymentReminders)
                .WithOne(e => e.ParentInvoice)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(t => t.Amount);
            builder.Ignore(e => e.IsPaid);

        }
    }
}