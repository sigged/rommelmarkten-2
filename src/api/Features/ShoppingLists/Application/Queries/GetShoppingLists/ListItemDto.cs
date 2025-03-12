using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Queries.GetShoppingLists
{
    public class ListItemDto : IMapFrom<ListItem>
    {
        public int Id { get; set; }

        public int ListId { get; set; }

        public int? CategoryId { get; set; }

        public required string Title { get; set; }

        public bool Done { get; set; }

    }
}
