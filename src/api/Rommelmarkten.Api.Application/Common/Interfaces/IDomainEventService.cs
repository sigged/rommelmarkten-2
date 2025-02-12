using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
