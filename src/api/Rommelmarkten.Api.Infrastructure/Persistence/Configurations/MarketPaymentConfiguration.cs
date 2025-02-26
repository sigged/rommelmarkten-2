using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Infrastructure.Persistence.Configurations
{
    public class MarketPaymentConfiguration : IEntityTypeConfiguration<MarketPayment>
    {
        public void Configure(EntityTypeBuilder<MarketPayment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Market)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<MarketPayment>(e => e.MarketId);

            builder.Property(t => t.Amount)
                .HasPrecision(18, 2);

            builder.Ignore(e => e.IsPaid);

        }
    }
}