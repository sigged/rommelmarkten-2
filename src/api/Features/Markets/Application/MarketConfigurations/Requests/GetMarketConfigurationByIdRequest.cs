using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Models;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Requests
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
