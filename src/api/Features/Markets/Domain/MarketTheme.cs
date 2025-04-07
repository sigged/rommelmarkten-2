using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Markets.Domain
{
    public class MarketTheme : EntityBase<Guid>
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string ImageUrl { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Market> Markets { get; set; } = [];

        public ICollection<MarketRevision> MarketRevisions { get; set; } = [];
    }

}
