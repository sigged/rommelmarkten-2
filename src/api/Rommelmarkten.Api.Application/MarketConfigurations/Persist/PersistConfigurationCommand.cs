using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations
{

    public class PersistConfigurationCommand : IRequest
    {
        public required MarketConfiguration Configuration { get; set; }
    }

    public class PersistConfigurationCommandHandler : IRequestHandler<PersistConfigurationCommand>
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IEntityRepository<MarketConfiguration> repository;

        public PersistConfigurationCommandHandler(IApplicationDbContext applicationDbContext, IEntityRepository<MarketConfiguration> repository)
        {
            this.applicationDbContext = applicationDbContext;
            this.repository = repository;
        }

        public async Task Handle(PersistConfigurationCommand request, CancellationToken cancellationToken)
        {
            var exists= await repository.AnyAsync(filters: [e => e.Id == request.Configuration.Id], cancellationToken);
            if (exists)
            {
                await repository.UpdateAsync(request.Configuration, cancellationToken);
            }
            else
            {
                await repository.InsertAsync(request.Configuration, cancellationToken);
            }
        }
    }

}
