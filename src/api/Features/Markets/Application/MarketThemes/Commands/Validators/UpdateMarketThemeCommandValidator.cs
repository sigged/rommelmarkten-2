using Rommelmarkten.Api.Features.Markets.Application.Gateways;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Commands.Validators
{

    public class UpdateMarketThemeCommandValidator : MarketThemeValidatorBase<UpdateMarketThemeCommand>
    {
        public UpdateMarketThemeCommandValidator(IMarketsDbContext context) : base(context)
        {
        }
    }
}
