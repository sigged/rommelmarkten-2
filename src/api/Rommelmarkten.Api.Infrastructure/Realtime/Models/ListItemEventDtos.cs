using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Application.ShoppingLists.Queries.GetShoppingLists;
using Rommelmarkten.Api.Domain.Events;

namespace Rommelmarkten.Api.Infrastructure.Realtime.Models
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

        public ListItemDto Item { get; set; }
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

        public ListItemDto Item { get; set; }
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

        public ListItemDto Item { get; set; }
    }

}
