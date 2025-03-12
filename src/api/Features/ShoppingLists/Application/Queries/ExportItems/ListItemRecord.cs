using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Queries.ExportItems
{
    public class ListItemRecord : IMapFrom<ListItem>
    {
        public required string Title { get; set; }

        public bool Done { get; set; }
    }
}
