﻿using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.MarketConfigurations.Models;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Commands
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
