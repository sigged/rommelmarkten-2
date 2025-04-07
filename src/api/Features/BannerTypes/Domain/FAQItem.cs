using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Domain
{
    public class FAQItem : EntityBase<Guid>
    {
        public required Guid CategoryId { get; set; }
        public FAQCategory? Category { get; set; }
        public required string Question { get; set; }
        public required string Answer { get; set; }
        public int Order { get; set; }

    }
}
