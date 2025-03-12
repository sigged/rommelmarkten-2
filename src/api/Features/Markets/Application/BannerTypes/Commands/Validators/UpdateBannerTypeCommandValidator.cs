using Rommelmarkten.Api.Features.Markets.Application.Gateways;

namespace Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Commands.Validators
{

    public class UpdateBannerTypeCommandValidator : BannerTypeValidatorBase<UpdateBannerTypeCommand>
    {
        public UpdateBannerTypeCommandValidator(IMarketsDbContext context) : base(context)
        {
        }
    }
}
