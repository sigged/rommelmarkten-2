using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.FAQCategories.Models
{
    public class FAQCategoryDto : IMapFrom<FAQCategory>
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public int Order { get; set; }
    }
}
