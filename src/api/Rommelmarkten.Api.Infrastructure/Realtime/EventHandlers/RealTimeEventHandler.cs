using AutoMapper;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Realtime.EventHandlers
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
