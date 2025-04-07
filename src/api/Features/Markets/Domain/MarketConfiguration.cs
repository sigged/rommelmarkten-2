using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Markets.Domain
{
    public class MarketConfiguration : EntityBase<Guid>
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public int MaximumThemes { get; set; }

        public int MaximumCharacters { get; set; }

        public bool AllowBanners { get; set; }

        public bool AllowPoster { get; set; }
    }
}
