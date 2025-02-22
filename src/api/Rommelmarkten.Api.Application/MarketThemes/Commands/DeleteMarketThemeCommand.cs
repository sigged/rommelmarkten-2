using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketThemes.Commands
{
    [Authorize(Policy = Policies.MustBeAdmin)]
    [CacheInvalidator(Tags = [CacheTagNames.MarketTheme])]
    public class DeleteMarketThemeCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteMarketThemeCommandHandler : IRequestHandler<DeleteMarketThemeCommand>
    {
        private readonly IEntityRepository<MarketTheme> repository;

        public DeleteMarketThemeCommandHandler(IEntityRepository<MarketTheme> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteMarketThemeCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.Id, cancellationToken);
        }
    }


}
