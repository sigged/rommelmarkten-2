using Rommelmarkten.Api.Features.Markets.Application.Gateways;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Commands.Validators
{

    public class UpdateMarketConfigurationCommandValidator : MarketConfigurationValidatorBase<UpdateMarketConfigurationCommand>
    {
        public UpdateMarketConfigurationCommandValidator(IMarketsDbContext context) : base(context)
        {
        }
    }
}
