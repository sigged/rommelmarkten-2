using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Queries.GetShoppingLists;
using Rommelmarkten.Api.Features.ShoppingLists.Events;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.Models
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

        public required ShoppingListDto List { get; set; }
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

        public required ShoppingListDto List { get; set; }
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

        public required ShoppingListDto List { get; set; }
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

        public required ShoppingListDto List { get; set; }

        public required UserDto Associate { get; set; }

        public bool Shared { get; set; }
    }
}
