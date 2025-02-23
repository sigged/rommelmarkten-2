using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.BannerTypes.Models
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
