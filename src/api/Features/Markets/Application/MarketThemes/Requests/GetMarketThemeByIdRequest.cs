using AutoMapper;
using MediatR;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Models;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Requests
{
    public struct GetMarketThemeByIdRequest : IRequest<MarketThemeDto>
    {
        public Guid Id { get; set; }
    }
    public class GetConfigurationByIdRequestHandler : IRequestHandler<GetMarketThemeByIdRequest, MarketThemeDto>
    {
        private readonly IEntityRepository<MarketTheme> repository;
        private readonly IMapper mapper;

        public GetConfigurationByIdRequestHandler(IEntityRepository<MarketTheme> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<MarketThemeDto> Handle(GetMarketThemeByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
            return mapper.Map<MarketThemeDto>(entity);
        }
    }

}
