using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Models;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Commands
{

    [Authorize(Policy = Policies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.MarketConfiguration])]
    public class UpdateMarketConfigurationCommand : MarketConfigurationDto, IRequest
    {
    }

    public class UpdateMarketConfigurationCommandHandler : IRequestHandler<UpdateMarketConfigurationCommand>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;

        public UpdateMarketConfigurationCommandHandler(IEntityRepository<MarketConfiguration> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateMarketConfigurationCommand request, CancellationToken cancellationToken)
        {
            var entity = new MarketConfiguration
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                IsActive = request.IsActive,
                MaximumThemes = request.MaximumThemes,
                MaximumCharacters = request.MaximumCharacters,
                AllowBanners = request.AllowBanners,
                AllowPoster = request.AllowPoster,
            };

            await repository.UpdateAsync(entity, cancellationToken);
        }
    }

}
