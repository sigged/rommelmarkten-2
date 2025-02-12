using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Application.ShoppingLists.Queries.ExportItems
{
    public class ListItemRecord : IMapFrom<ListItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
