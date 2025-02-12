using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.ShoppingLists.Queries.GetShoppingLists;
using Rommelmarkten.Api.Domain.Events;

namespace Rommelmarkten.Api.Infrastructure.Realtime.Models
{
    public record ShoppingListCreatedEventDto : EventBaseDto, IMapFrom<ShoppingListCreatedEvent>
    {
        public ShoppingListCreatedEventDto() : base()
        {
        }

        public ShoppingListCreatedEventDto(ShoppingListDto list) : base()
        {
            List = list;
        }

        public ShoppingListDto List { get; set; }
    }

    public record ShoppingListRemovedEventDto : EventBaseDto, IMapFrom<ShoppingListRemovedEvent>
    {
        public ShoppingListRemovedEventDto() : base()
        {
        }

        public ShoppingListRemovedEventDto(ShoppingListDto list) : base()
        {
            List = list;
        }

        public ShoppingListDto List { get; set; }
    }

    public record ShoppingListUpdatedEventDto : EventBaseDto, IMapFrom<ShoppingListUpdatedEvent>
    {
        public ShoppingListUpdatedEventDto() : base()
        {
        }

        public ShoppingListUpdatedEventDto(ShoppingListDto list) : base()
        {
            List = list;
        }

        public ShoppingListDto List { get; set; }
    }

    public record ShoppingListShareEventDto : EventBaseDto, IMapFrom<ShoppingListShareEvent>
    {
        public ShoppingListShareEventDto() : base()
        {
        }

        public ShoppingListShareEventDto(ShoppingListDto list, UserDto associate, bool shared) : base()
        {
            List = list;
            Associate = associate;
            Shared = shared;
        }

        public ShoppingListDto List { get; set; }

        public UserDto Associate { get; set; }

        public bool Shared { get; set; }
    }
}
