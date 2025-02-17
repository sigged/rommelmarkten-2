using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.MarketConfigurations.Models;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Requests
{
    public struct GetMarketConfigurationByIdRequest : IRequest<MarketConfigurationDto>
    {
        public Guid Id { get; set; }
    }
    public class GetConfigurationByIdRequestHandler : IRequestHandler<GetMarketConfigurationByIdRequest, MarketConfigurationDto>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;
        private readonly IMapper mapper;

        public GetConfigurationByIdRequestHandler(IEntityRepository<MarketConfiguration> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MarketConfigurationDto> Handle(GetMarketConfigurationByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
            return mapper.Map<MarketConfigurationDto>(entity);
        }
    }

}
