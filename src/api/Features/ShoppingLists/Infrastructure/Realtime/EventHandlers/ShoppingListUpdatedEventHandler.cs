using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.ShoppingLists.Events;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.Models;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.EventHandlers
{
    public class ShoppingListUpdatedEventHandler : RealTimeEventHandler<ShoppingListUpdatedEvent>
    {
        public ShoppingListUpdatedEventHandler(IHubContext<FetchHub> hubContext, IMapper mapper) : base(hubContext, mapper) { }

        public override async Task Handle(DomainEventNotification<ShoppingListUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var groupName = FetchHubHelpers.GetShoppingListGroupName(domainEvent.List.Id.ToString());

            var dto = _mapper.Map<ShoppingListUpdatedEventDto>(notification.DomainEvent);
            await _hubContext.ClientMethods().OnListUpdated(groupName, dto);
        }
    }
}
