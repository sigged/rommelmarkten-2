using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Commands
{

    public class CreateMarketConfigurationCommand : IRequest
    {
        public required MarketConfiguration Configuration { get; set; }
    }

    public class CreateMarketConfigurationCommandHandler : IRequestHandler<CreateMarketConfigurationCommand>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;

        public CreateMarketConfigurationCommandHandler(IEntityRepository<MarketConfiguration> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(CreateMarketConfigurationCommand request, CancellationToken cancellationToken)
        {
            await repository.InsertAsync(request.Configuration, cancellationToken);
        }
    }
}
