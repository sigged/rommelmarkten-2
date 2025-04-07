using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.ShoppingLists.Events;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.Models;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.EventHandlers
{
    public class ShoppingListShareEventHandler : RealTimeEventHandler<ShoppingListShareEvent>
    {
        public ShoppingListShareEventHandler(IHubContext<FetchHub> hubContext, IMapper mapper) : base(hubContext, mapper) { }

        public override async Task Handle(DomainEventNotification<ShoppingListShareEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var groupName = FetchHubHelpers.GetShoppingListGroupName(domainEvent.List.Id.ToString());

            var dto = _mapper.Map<ShoppingListShareEventDto>(notification.DomainEvent);
            await _hubContext.ClientMethods().OnListShared(groupName, dto);
        }
    }

}
