using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Models
{
    public class BannerTypeDto : IMapFrom<BannerType>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }
}
