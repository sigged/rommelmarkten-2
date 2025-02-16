using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.GetPaged
{
    public class GetPagedConfigurationsResult
    {
        public required PaginatedList<MarketConfiguration> Results { get; set; }

    }


}
