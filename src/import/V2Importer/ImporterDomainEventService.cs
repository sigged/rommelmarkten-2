using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Domain;

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
