using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.MarketThemes.Models;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketThemes.Commands
{
    [Authorize(Policy=Policies.MustBeAdmin)]
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
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                IsDefault = request.IsDefault,
                IsActive = request.IsActive,
            };

            await repository.InsertAsync(entity, cancellationToken);

            return createdId;
        }
    }
}
