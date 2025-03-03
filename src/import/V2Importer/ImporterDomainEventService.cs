using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Common;

namespace V2Importer
{
    public class ImporterDomainEventService : IDomainEventService
    {
        public Task Publish(DomainEvent domainEvent)
        {
            return Task.CompletedTask;
        }
    }
}
