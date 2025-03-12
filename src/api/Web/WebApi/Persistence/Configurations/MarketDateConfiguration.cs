using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.WebApi.Persistence.Configurations
{
    public class MarketDateConfiguration : IEntityTypeConfiguration<MarketDate>
    {
        public void Configure(EntityTypeBuilder<MarketDate> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(t => t.Date)
                .IsRequired();

            builder.HasOne(d => d.ParentMarket)
                   .WithMany(m => m.Dates);

        }
    }
}