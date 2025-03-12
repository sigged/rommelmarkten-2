using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.ShoppingLists.Events;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.Models;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.EventHandlers
{

    public class ListItemUpdatedEventHandler : RealTimeEventHandler<ListItemUpdatedEvent>
    {
        public ListItemUpdatedEventHandler(IHubContext<FetchHub> hubContext, IMapper mapper) : base(hubContext, mapper) { }

        public override async Task Handle(DomainEventNotification<ListItemUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var groupName = FetchHubHelpers.GetShoppingListGroupName(domainEvent.Item.ListId.ToString());

            var dto = _mapper.Map<ListItemUpdatedEventDto>(notification.DomainEvent);
            await _hubContext.ClientMethods().OnListItemUpdated(groupName, dto);
        }
    }

}
