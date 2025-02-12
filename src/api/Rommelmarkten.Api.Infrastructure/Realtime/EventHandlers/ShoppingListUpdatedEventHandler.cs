using AutoMapper;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Events;
using Rommelmarkten.Api.Infrastructure.Realtime.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Realtime.EventHandlers
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
