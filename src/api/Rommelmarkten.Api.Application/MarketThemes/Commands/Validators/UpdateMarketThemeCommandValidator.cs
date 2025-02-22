using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.MarketThemes.Commands.Validators
{

    public class UpdateMarketThemeCommandValidator : MarketThemeValidatorBase<UpdateMarketThemeCommand>
    {
        public UpdateMarketThemeCommandValidator(IApplicationDbContext context) : base(context)
        {
        }
    }
}
