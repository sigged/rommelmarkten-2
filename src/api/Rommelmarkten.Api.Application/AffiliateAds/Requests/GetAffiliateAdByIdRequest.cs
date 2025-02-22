using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Application.AffiliateAds.Models;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Affiliates;

namespace Rommelmarkten.Api.Application.AffiliateAds.Requests
{
    public struct GetAffiliateAdByIdRequest : IRequest<AffiliateAdDto>
    {
        public Guid Id { get; set; }
    }
    public class GetConfigurationByIdRequestHandler : IRequestHandler<GetAffiliateAdByIdRequest, AffiliateAdDto>
    {
        private readonly IEntityRepository<AffiliateAd> repository;
        private readonly IMapper mapper;

        public GetConfigurationByIdRequestHandler(IEntityRepository<AffiliateAd> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<AffiliateAdDto> Handle(GetAffiliateAdByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
            return mapper.Map<AffiliateAdDto>(entity);
        }
    }

}
