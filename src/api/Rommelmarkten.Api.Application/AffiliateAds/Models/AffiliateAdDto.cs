using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Affiliates;

namespace Rommelmarkten.Api.Application.AffiliateAds.Models
{
    public class AffiliateAdDto : IMapFrom<AffiliateAd>
    {
        public Guid Id { get; set; }

        public required string ImageUrl { get; set; }

        public required string AffiliateName { get; set; }

        public required string AffiliateURL { get; set; }

        public string? AdContent { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }
    }
}
