using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Application.ListItems.EventHandlers
{


    public class ListItemCreatedEventHandler : INotificationHandler<DomainEventNotification<ListItemCreatedEvent>>
    {
        private readonly ILogger<ListItemCreatedEventHandler> _logger;

        public ListItemCreatedEventHandler(ILogger<ListItemCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(DomainEventNotification<ListItemCreatedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            _logger.LogInformation("Rommelmarkten API Domain Event: {DomainEvent}", domainEvent.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
