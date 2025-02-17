using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Commands
{
    [Authorize(Policy = Policies.MustBeAdmin)]
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
