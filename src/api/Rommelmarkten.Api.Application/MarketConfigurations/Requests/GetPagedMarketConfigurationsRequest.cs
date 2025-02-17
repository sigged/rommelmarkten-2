using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.Common;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.Common.Validators;
using Rommelmarkten.Api.Application.MarketConfigurations.Models;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Requests
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
