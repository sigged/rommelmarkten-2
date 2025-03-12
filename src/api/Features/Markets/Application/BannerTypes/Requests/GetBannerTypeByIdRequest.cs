using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Models;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Requests
{
    public struct GetBannerTypeByIdRequest : IRequest<BannerTypeDto>
    {
        public Guid Id { get; set; }
    }
    public class GetConfigurationByIdRequestHandler : IRequestHandler<GetBannerTypeByIdRequest, BannerTypeDto>
    {
        private readonly IEntityRepository<BannerType> repository;
        private readonly IMapper mapper;

        public GetConfigurationByIdRequestHandler(IEntityRepository<BannerType> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BannerTypeDto> Handle(GetBannerTypeByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
            return mapper.Map<BannerTypeDto>(entity);
        }
    }

}
