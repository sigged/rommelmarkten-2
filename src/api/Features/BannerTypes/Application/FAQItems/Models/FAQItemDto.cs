using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQItems.Models
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
