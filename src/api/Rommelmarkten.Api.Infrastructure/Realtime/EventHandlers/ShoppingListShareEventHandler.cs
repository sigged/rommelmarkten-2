using AutoMapper;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Events;
using Rommelmarkten.Api.Infrastructure.Realtime.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Realtime.EventHandlers
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
