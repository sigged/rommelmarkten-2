using AutoMapper;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Events;
using Rommelmarkten.Api.Infrastructure.Realtime.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Realtime.EventHandlers
{
    public class ListItemCreatedEventHandler : RealTimeEventHandler<ListItemCreatedEvent>
    {
        public ListItemCreatedEventHandler(IHubContext<FetchHub> hubContext, IMapper mapper) : base(hubContext, mapper) { }

        public override async Task Handle(DomainEventNotification<ListItemCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var groupName = FetchHubHelpers.GetShoppingListGroupName(domainEvent.Item.ListId.ToString());

            var dto = _mapper.Map<ListItemCreatedEventDto>(notification.DomainEvent);
            await _hubContext.ClientMethods().OnListItemCreated(groupName, dto);
        }
    }

}
