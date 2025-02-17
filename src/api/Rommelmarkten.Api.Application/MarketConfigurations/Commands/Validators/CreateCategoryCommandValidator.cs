using Rommelmarkten.Api.Application.Common.Interfaces;
using System.Xml.Linq;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Commands.Validators
{

    public class UpdateMarketConfigurationCommandValidator : MarketConfigurationValidatorBase<UpdateMarketConfigurationCommand>
    {
        public UpdateMarketConfigurationCommandValidator(IApplicationDbContext context) : base(context)
        {
        }
    }
}
