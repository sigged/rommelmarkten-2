using MediatR;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.GetPaged
{
    public class GetPagedConfigurationsRequestHandler : IRequestHandler<GetPagedConfigurationsRequest, GetPagedConfigurationsResult>
    {
        private readonly IEntityRepository<MarketConfiguration> repository;

        public GetPagedConfigurationsRequestHandler(IEntityRepository<MarketConfiguration> repository)
        {
            this.repository = repository;
        }

        public async Task<GetPagedConfigurationsResult> Handle(GetPagedConfigurationsRequest request, CancellationToken cancellationToken)
        {
            var pagedResult = await repository.SelectPagedAsync(
                request.PagedRequest.PageIndex,
                request.PagedRequest.PageSize,
                orderBy: e => e.OrderBy(e => e.Name),
                cancellationToken: cancellationToken);

            return new GetPagedConfigurationsResult
            {
                Results = pagedResult
            };
        }
    }


}
