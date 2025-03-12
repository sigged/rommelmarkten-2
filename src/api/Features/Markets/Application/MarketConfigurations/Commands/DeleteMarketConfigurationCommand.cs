using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Commands
{
    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.MarketConfiguration])]
    public class DeleteMarketConfigurationCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteMarketConfigurationCommandHandler : IRequestHandler<DeleteMarketConfigurationCommand>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;

        public DeleteMarketConfigurationCommandHandler(IEntityRepository<MarketConfiguration> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteMarketConfigurationCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.Id, cancellationToken);
        }
    }


}
