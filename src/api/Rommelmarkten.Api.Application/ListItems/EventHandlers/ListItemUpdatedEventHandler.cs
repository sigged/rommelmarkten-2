using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Application.ListItems.EventHandlers
{
    public class ListItemUpdatedEventHandler : INotificationHandler<DomainEventNotification<ListItemUpdatedEvent>>
    {
        private readonly ILogger<ListItemUpdatedEventHandler> _logger;

        public ListItemUpdatedEventHandler(ILogger<ListItemUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ListItemUpdatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Mde.Fetch.Api Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
