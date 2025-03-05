using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Markets
{
    public class MarketRevision : AuditableEntity<Guid>
    {
        public Market? RevisedMarket { get; set; }

        public Guid RevisedMarketId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public ICollection<MarketTheme> Themes { get; set; } = [];

        public required MarketPricing Pricing { get; set; }

        public required MarketLocation Location { get; set; }

        public required MarketImage Image { get; set; }

        public required Organizer Organizer { get; set; }

    }
}
