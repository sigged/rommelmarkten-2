using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Queries.GetShoppingLists;
using Rommelmarkten.Api.Features.ShoppingLists.Events;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.Models
{
    public record ListItemCreatedEventDto : EventBaseDto, IMapFrom<ListItemCreatedEvent>
    {
        public ListItemCreatedEventDto() : base()
        {
        }

        public ListItemCreatedEventDto(ListItemDto listItem) : base()
        {
            Item = listItem;
        }

        public required ListItemDto Item { get; set; }
    }
    public record ListItemUpdatedEventDto : EventBaseDto, IMapFrom<ListItemUpdatedEvent>
    {
        public ListItemUpdatedEventDto() : base()
        {
        }

        public ListItemUpdatedEventDto(ListItemDto listItem) : base()
        {
            Item = listItem;
        }

        public required ListItemDto Item { get; set; }
    }

    public record ListItemRemovedEventDto : EventBaseDto, IMapFrom<ListItemRemovedEvent>
    {
        public ListItemRemovedEventDto() : base()
        {
        }

        public ListItemRemovedEventDto(ListItemDto listItem) : base()
        {
            Item = listItem;
        }

        public required ListItemDto Item { get; set; }
    }

}
