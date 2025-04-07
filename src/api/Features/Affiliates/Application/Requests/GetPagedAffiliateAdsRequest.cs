using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Pagination;
using Rommelmarkten.Api.Features.Affiliates.Application.Models;
using Rommelmarkten.Api.Features.Affiliates.Domain;

namespace Rommelmarkten.Api.Features.Affiliates.Application.Requests
{
    public class GetPagedAffiliateAdsRequest : PaginatedRequest, IRequest<PaginatedList<AffiliateAdDto>>
    {
    }

    public class GetPagedAffiliateAdsRequestValidator : PaginatedRequestValidatorBase<GetPagedAffiliateAdsRequest>
    {
    }

    public class GetPagedAffiliateAdsRequestHandler : IRequestHandler<GetPagedAffiliateAdsRequest, PaginatedList<AffiliateAdDto>>
    {
        private readonly IEntityRepository<AffiliateAd> repository;
        private readonly IConfigurationProvider mapperConfiguration;

        public GetPagedAffiliateAdsRequestHandler(IEntityRepository<AffiliateAd> repository, IConfigurationProvider mapperConfiguration)
        {
            this.repository = repository;
            this.mapperConfiguration = mapperConfiguration;
        }

        public async Task<PaginatedList<AffiliateAdDto>> Handle(GetPagedAffiliateAdsRequest request, CancellationToken cancellationToken)
        {

            var query = repository.SelectAsQuery(
                orderBy: e => e.OrderBy(e => e.Order)
            );

            var result = await query.ToPagesAsync<AffiliateAd, AffiliateAdDto>(request.PageNumber, request.PageSize, mapperConfiguration);
            return result;
        }
    }


}
