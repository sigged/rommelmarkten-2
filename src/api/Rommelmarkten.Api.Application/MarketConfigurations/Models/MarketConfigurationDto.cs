﻿using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Models
{
    public class MarketConfigurationDto : IMapFrom<MarketConfiguration>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string? Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public int MaximumThemes { get; set; }

        public int MaximumCharacters { get; set; }

        public bool AllowBanners { get; set; }

        public bool AllowPoster { get; set; }
    }
}
