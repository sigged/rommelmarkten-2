using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Affiliates.Domain
{
    public class AffiliateAd : AuditableEntity<Guid>
    {
        public required string ImageUrl { get; set; }

        public required string AffiliateName { get; set; }

        public required string AffiliateURL { get; set; }

        public string? AdContent { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }
    }
}
