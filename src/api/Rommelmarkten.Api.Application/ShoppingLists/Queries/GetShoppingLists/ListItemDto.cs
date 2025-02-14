using AutoMapper;
using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Application.ShoppingLists.Queries.GetShoppingLists
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
