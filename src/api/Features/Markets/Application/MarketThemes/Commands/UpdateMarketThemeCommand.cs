using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Models;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Commands
{

    [Authorize(Policy = CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.MarketTheme])]
    public class UpdateMarketThemeCommand : MarketThemeDto, IRequest
    {
    }

    public class UpdateMarketThemeCommandHandler : IRequestHandler<UpdateMarketThemeCommand>
    {
        private readonly IEntityRepository<MarketTheme> repository;

        public UpdateMarketThemeCommandHandler(IEntityRepository<MarketTheme> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateMarketThemeCommand request, CancellationToken cancellationToken)
        {
            var entity = new MarketTheme
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                IsDefault = request.IsDefault,
                IsActive = request.IsActive,
            };

            await repository.UpdateAsync(entity, cancellationToken);
        }
    }

}
