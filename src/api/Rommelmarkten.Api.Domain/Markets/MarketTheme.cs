using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Markets
{
    public class MarketTheme : EntityBase<Guid>
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Market> Markets { get; set; } = [];

        //public ICollection<MarketRevision> MarketRevisions { get; set; } = [];
    }

}
