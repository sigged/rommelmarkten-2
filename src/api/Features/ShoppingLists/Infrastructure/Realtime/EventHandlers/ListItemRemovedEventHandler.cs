using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Features.ShoppingLists.Events;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.Models;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.EventHandlers
{
    public class ListItemRemovedEventHandler : RealTimeEventHandler<ListItemRemovedEvent>
    {
        public ListItemRemovedEventHandler(IHubContext<FetchHub> hubContext, IMapper mapper) : base(hubContext, mapper) { }

        public override async Task Handle(DomainEventNotification<ListItemRemovedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var groupName = FetchHubHelpers.GetShoppingListGroupName(domainEvent.Item.ListId.ToString());

            var dto = _mapper.Map<ListItemRemovedEventDto>(notification.DomainEvent);
            await _hubContext.ClientMethods().OnListItemRemoved(groupName, dto);
        }
    }

}
