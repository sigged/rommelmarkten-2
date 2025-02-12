using AutoMapper;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Events;
using Rommelmarkten.Api.Infrastructure.Realtime.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Realtime.EventHandlers
{
    public class ShoppingListCreatedEventHandler : RealTimeEventHandler<ShoppingListCreatedEvent>
    {
        public ShoppingListCreatedEventHandler(IHubContext<FetchHub> hubContext, IMapper mapper) : base(hubContext, mapper) { }

        public override async Task Handle(DomainEventNotification<ShoppingListCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var groupName = FetchHubHelpers.GetShoppingListGroupName(domainEvent.List.Id.ToString());

            var dto = _mapper.Map<ShoppingListCreatedEventDto>(notification.DomainEvent);
            await _hubContext.ClientMethods().OnListCreated(groupName, dto);
        }
    }
}
