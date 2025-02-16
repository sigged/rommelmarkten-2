using MediatR;
using Rommelmarkten.Api.Domain.Markets;

namespace Rommelmarkten.Api.Application.MarketConfigurations.GetById
{
    public struct GetConfigurationByIdRequest : IRequest<MarketConfiguration>
    {
        public Guid Id { get; set; }
    }

}
