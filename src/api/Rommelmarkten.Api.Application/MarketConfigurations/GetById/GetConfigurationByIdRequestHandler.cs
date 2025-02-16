using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.GetById
{
    public class GetConfigurationByIdRequestHandler : IRequestHandler<GetConfigurationByIdRequest, MarketConfiguration>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;

        public GetConfigurationByIdRequestHandler(IEntityRepository<MarketConfiguration> repository)
        {
            this.repository = repository;
        }

        public async Task<MarketConfiguration> Handle(GetConfigurationByIdRequest request, CancellationToken cancellationToken)
        {
            return await repository.GetByIdAsync(request.Id, cancellationToken);
        }
    }

}
