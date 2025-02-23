using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Content
{
    public class FAQCategory : EntityBase<Guid>
    {
        public ICollection<FAQItem> FAQItems { get; set; } = [];

        public required string Name { get; set; }

        public int Order { get; set; }
    }

}
