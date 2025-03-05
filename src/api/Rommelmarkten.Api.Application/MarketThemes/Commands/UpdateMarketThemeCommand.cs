using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.MarketThemes.Models;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketThemes.Commands
{

    [Authorize(Policy = Policies.MustBeAdmin)]
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
