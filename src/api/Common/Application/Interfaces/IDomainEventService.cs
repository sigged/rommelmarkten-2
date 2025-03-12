using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Common.Application.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
