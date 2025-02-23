using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Content
{
    public class FAQItem : EntityBase<Guid>
    {

        public required FAQCategory Category { get; set; }

        public required string Question { get; set; }

        public required string Answer { get; set; }

        public int Order { get; set; }

    }
}
