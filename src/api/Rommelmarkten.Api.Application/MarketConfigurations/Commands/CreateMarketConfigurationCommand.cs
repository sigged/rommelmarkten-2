﻿using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.MarketConfigurations.Models;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Commands
{
    [Authorize(Policy=Policies.MustBeAdmin)]
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
