using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Pagination;
using Rommelmarkten.Api.Application.BannerTypes.Models;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.BannerTypes.Requests
{
    public class GetPagedBannerTypesRequest : PaginatedRequest, IRequest<PaginatedList<BannerTypeDto>>
    {
    }

    public class GetPagedBannerTypesRequestValidator : PaginatedRequestValidatorBase<GetPagedBannerTypesRequest>
    {
    }

    public class GetPagedBannerTypesRequestHandler : IRequestHandler<GetPagedBannerTypesRequest, PaginatedList<BannerTypeDto>>
    {
        private readonly IEntityRepository<BannerType> repository;
        private readonly IConfigurationProvider mapperConfiguration;

        public GetPagedBannerTypesRequestHandler(IEntityRepository<BannerType> repository, IConfigurationProvider mapperConfiguration)
        {
            this.repository = repository;
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<PaginatedList<BannerTypeDto>> Handle(GetPagedBannerTypesRequest request, CancellationToken cancellationToken)
        {

            var query = repository.SelectAsQuery(
                orderBy: e => e.OrderBy(e => e.Name)
            );

            var result = await query.ToPagesAsync<BannerType, BannerTypeDto>(request.PageNumber, request.PageSize, mapperConfiguration);
            return result;
        }
    }


}
