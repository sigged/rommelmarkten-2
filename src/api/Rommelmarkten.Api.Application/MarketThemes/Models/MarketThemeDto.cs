﻿using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketThemes.Models
{
    public class MarketThemeDto : IMapFrom<MarketTheme>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActive { get; set; }
    }
}
