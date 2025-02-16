using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Commands
{
    public class DeleteConfigurationCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class DeleteConfigurationCommandHandler : IRequestHandler<DeleteConfigurationCommand>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;

        public DeleteConfigurationCommandHandler(IEntityRepository<MarketConfiguration> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(DeleteConfigurationCommand request, CancellationToken cancellationToken)
        {
            await repository.DeleteByIdAsync(request.Id, cancellationToken);
        }
    }

}
