using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Affiliates
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
