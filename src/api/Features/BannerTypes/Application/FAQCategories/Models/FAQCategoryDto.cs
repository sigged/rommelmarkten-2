using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Models
{
    public class FAQCategoryDto : IMapFrom<FAQCategory>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public int Order { get; set; }
    }
}
