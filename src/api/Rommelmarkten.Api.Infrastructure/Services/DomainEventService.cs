using MediatR;
using Microsoft.Extensions.Logging;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;
        private readonly IPublisher _mediator;

        public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
            if(notificationType != null)
            {
                var notificationInstance = Activator.CreateInstance(notificationType, domainEvent);
                if(notificationInstance != null)
                {
                    return (INotification)notificationInstance;
                }
                else
                {
                    throw new ApplicationException("Notification instance could not be created for domain event");
                }
            }
            else
            {
                throw new ApplicationException("Notification type not found for domain event");
            }
                
        }
    }
}