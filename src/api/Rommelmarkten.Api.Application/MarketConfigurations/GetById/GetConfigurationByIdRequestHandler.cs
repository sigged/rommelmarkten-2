using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Exceptions;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.GetById
{
    public class GetConfigurationByIdRequestHandler : IRequestHandler<GetConfigurationByIdRequest, MarketConfiguration>
    {
        private readonly IApplicationDbContext dbContext;

        public GetConfigurationByIdRequestHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<MarketConfiguration> Handle(GetConfigurationByIdRequest request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.MarketConfigurations.FirstOrDefaultAsync(e => e.Id.Equals(request.Id), cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException($"{nameof(MarketConfiguration)} identified as {nameof(MarketConfiguration.Id)} was not found");
            }
            return entity;
        }
    }

}
