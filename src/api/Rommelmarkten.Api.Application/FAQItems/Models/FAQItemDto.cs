using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQItems.Models
{
    public class FAQItemDto : IMapFrom<FAQItem>
    {
        public Guid Id { get; set; }

        public required Guid CategoryId { get; set; }

        public required string Question { get; set; }

        public required string Answer { get; set; }

        public int Order { get; set; }
    }
}
