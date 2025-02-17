using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Requests
{
    public struct GetMarketConfigurationByIdRequest : IRequest<MarketConfiguration>
    {
        public Guid Id { get; set; }
    }
    public class GetConfigurationByIdRequestHandler : IRequestHandler<GetMarketConfigurationByIdRequest, MarketConfiguration>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;

        public GetConfigurationByIdRequestHandler(IEntityRepository<MarketConfiguration> repository)
        {
            this.repository = repository;
        }

        public async Task<MarketConfiguration> Handle(GetMarketConfigurationByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
            return entity;
        }
    }

}
