using MediatR;

namespace Rommelmarkten.Api.Application.Markets.Persist
{
    public class PersistMarketCommandHandler : IRequestHandler<PersistMarketCommand, Guid>
    {
        public PersistMarketCommandHandler()
        {
        }

        public async Task<Guid> Handle(PersistMarketCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException("PersistMarket is not implemented");
            await Task.CompletedTask;
            return default;
        }
    }

}
