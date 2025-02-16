using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Commands
{

    public class UpdateMarketConfigurationCommand : IRequest
    {
        public required MarketConfiguration Configuration { get; set; }
    }

    public class UpdateMarketConfigurationCommandHandler : IRequestHandler<UpdateMarketConfigurationCommand>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;

        public UpdateMarketConfigurationCommandHandler(IEntityRepository<MarketConfiguration> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateMarketConfigurationCommand request, CancellationToken cancellationToken)
        {
            await repository.UpdateAsync(request.Configuration, cancellationToken);
        }
    }

}
