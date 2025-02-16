using MediatR;
using Rommelmarkten.Api.Application.Common.Models;

namespace Rommelmarkten.Api.Application.MarketConfigurations.GetPaged
{
    public class GetPagedConfigurationsRequest : IRequest<GetPagedConfigurationsResult>
    {
        public PaginatedRequest PagedRequest { get; set; }
    }


}
