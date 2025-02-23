using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Pagination;
using Rommelmarkten.Api.Application.MarketThemes.Models;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketThemes.Requests
{
    public class GetPagedMarketThemesRequest : PaginatedRequest, IRequest<PaginatedList<MarketThemeDto>>
    {
    }

    public class GetPagedMarketThemesRequestValidator : PaginatedRequestValidatorBase<GetPagedMarketThemesRequest>
    {
    }

    public class GetPagedMarketThemesRequestHandler : IRequestHandler<GetPagedMarketThemesRequest, PaginatedList<MarketThemeDto>>
    {
        private readonly IEntityRepository<MarketTheme> repository;
        private readonly IConfigurationProvider mapperConfiguration;

        public GetPagedMarketThemesRequestHandler(IEntityRepository<MarketTheme> repository, IConfigurationProvider mapperConfiguration)
        {
            this.repository = repository;
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<PaginatedList<MarketThemeDto>> Handle(GetPagedMarketThemesRequest request, CancellationToken cancellationToken)
        {

            var query = repository.SelectAsQuery(
                orderBy: e => e.OrderBy(e => e.Name)
            );

            var result = await query.ToPagesAsync<MarketTheme, MarketThemeDto>(request.PageNumber, request.PageSize, mapperConfiguration);
            return result;
        }
    }


}
