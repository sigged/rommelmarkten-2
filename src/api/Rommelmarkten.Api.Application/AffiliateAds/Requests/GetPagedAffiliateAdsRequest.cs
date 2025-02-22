using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.AffiliateAds.Models;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Pagination;
using Rommelmarkten.Api.Domain.Affiliates;

namespace Rommelmarkten.Api.Application.AffiliateAds.Requests
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
