using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Models;
using Rommelmarkten.Api.Features.Markets.Domain;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Commands
{
    [Authorize(Policy=CorePolicies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.MarketTheme])]
    public class CreateMarketThemeCommand : MarketThemeDto, IRequest<Guid>
    {
    }

    public class CreateMarketThemeCommandHandler : IRequestHandler<CreateMarketThemeCommand, Guid>
    {
        private readonly IEntityRepository<MarketTheme> repository;

        public CreateMarketThemeCommandHandler(IEntityRepository<MarketTheme> repository)
        {
            this.repository = repository;
        }

        public async Task<Guid> Handle(CreateMarketThemeCommand request, CancellationToken cancellationToken)
        {
            Guid createdId = Guid.NewGuid();

            var entity = new MarketTheme {
                Id = createdId,
                Name = request.Name,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                IsDefault = request.IsDefault,
                IsActive = request.IsActive,
            };

            await repository.InsertAsync(entity, cancellationToken);

            return createdId;
        }
    }
}
