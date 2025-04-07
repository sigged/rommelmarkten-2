using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Models;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Commands
{
    [Authorize(Policy=CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.MarketConfiguration])]
    public class CreateMarketConfigurationCommand : MarketConfigurationDto, IRequest<Guid>
    {
    }

    public class CreateMarketConfigurationCommandHandler : IRequestHandler<CreateMarketConfigurationCommand, Guid>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;

        public CreateMarketConfigurationCommandHandler(IEntityRepository<MarketConfiguration> repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(CreateMarketConfigurationCommand request, CancellationToken cancellationToken)
        {
            Guid createdId = Guid.NewGuid();

            var entity = new MarketConfiguration {
                Id = createdId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                IsActive = request.IsActive,
                MaximumThemes = request.MaximumThemes,
                MaximumCharacters = request.MaximumCharacters,
                AllowBanners = request.AllowBanners,
                AllowPoster = request.AllowPoster,
            };

            await repository.InsertAsync(entity, cancellationToken);

            return createdId;
        }
    }
}
