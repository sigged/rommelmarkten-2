using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.ShoppingLists.Events;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.Models;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.EventHandlers
{
    public class ShoppingListRemovedEventHandler : RealTimeEventHandler<ShoppingListRemovedEvent>
    {
        public ShoppingListRemovedEventHandler(IHubContext<FetchHub> hubContext, IMapper mapper) : base(hubContext, mapper) { }

        public override async Task Handle(DomainEventNotification<ShoppingListRemovedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var groupName = FetchHubHelpers.GetShoppingListGroupName(domainEvent.List.Id.ToString());

            var dto = _mapper.Map<ShoppingListRemovedEventDto>(notification.DomainEvent);
            await _hubContext.ClientMethods().OnListRemoved(groupName, dto);
        }
    }
}
