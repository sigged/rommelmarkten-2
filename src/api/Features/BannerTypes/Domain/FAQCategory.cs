using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Domain
{
    public class FAQCategory : EntityBase<Guid>
    {
        public ICollection<FAQItem> FAQItems { get; set; } = [];

        public required string Name { get; set; }

        public int Order { get; set; }
    }

}
