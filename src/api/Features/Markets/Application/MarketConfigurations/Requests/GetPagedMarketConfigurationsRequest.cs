using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Models;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Requests
{
    public class GetPagedMarketConfigurationsRequest : PaginatedRequest, IRequest<PaginatedList<MarketConfigurationDto>>
    {
    }

    public class GetPagedMarketConfigurationsRequestValidator : PaginatedRequestValidatorBase<GetPagedMarketConfigurationsRequest>
    {
    }

    public class GetPagedMarketConfigurationsRequestHandler : IRequestHandler<GetPagedMarketConfigurationsRequest, PaginatedList<MarketConfigurationDto>>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;
        private readonly IConfigurationProvider mapperConfiguration;

        public GetPagedMarketConfigurationsRequestHandler(IEntityRepository<MarketConfiguration> repository, IConfigurationProvider mapperConfiguration)
        {
            this.repository = repository;
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<PaginatedList<MarketConfigurationDto>> Handle(GetPagedMarketConfigurationsRequest request, CancellationToken cancellationToken)
        {

            var query = repository.SelectAsQuery(
                orderBy: e => e.OrderBy(e => e.Name)
            );

            var result = await query.ToPagesAsync<MarketConfiguration, MarketConfigurationDto>(request.PageNumber, request.PageSize, mapperConfiguration);
            return result;
        }
    }


}
