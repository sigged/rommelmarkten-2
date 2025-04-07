using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.EventHandlers
{
    public abstract class RealTimeEventHandler<TDomainEvent> : INotificationHandler<DomainEventNotification<TDomainEvent>>
        where TDomainEvent : DomainEvent
    {
        protected readonly IHubContext<FetchHub> _hubContext;
        protected readonly IMapper _mapper;

        public RealTimeEventHandler(IHubContext<FetchHub> hubContext, IMapper mapper)
        {
            _hubContext = hubContext;
            _mapper = mapper;
        }

        public abstract Task Handle(DomainEventNotification<TDomainEvent> notification, CancellationToken cancellationToken);

    }
}
