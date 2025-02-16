using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Exceptions;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.DeleteConfiguration
{
    public class DeleteConfigurationCommandHandler : IRequestHandler<DeleteConfigurationCommand>
    {
        private readonly IApplicationDbContext dbContext;

        public DeleteConfigurationCommandHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(DeleteConfigurationCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.MarketConfigurations.FirstOrDefaultAsync(e => e.Equals(request.Id), cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException($"{nameof(MarketConfiguration)} identified as {nameof(MarketConfiguration.Id)} was not found");
            }
            dbContext.MarketConfigurations.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

}
