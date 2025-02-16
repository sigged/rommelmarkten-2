using MediatR;

namespace Rommelmarkten.Api.Application.MarketConfigurations.DeleteConfiguration
{
    public class DeleteConfigurationCommand : IRequest
    {
        public Guid Id { get; set; }
    }

}
