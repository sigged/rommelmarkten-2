using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Models
{
    public class MarketThemeDto : IMapFrom<MarketTheme>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Description { get; set; }

        public required string ImageUrl { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }
    }
}
