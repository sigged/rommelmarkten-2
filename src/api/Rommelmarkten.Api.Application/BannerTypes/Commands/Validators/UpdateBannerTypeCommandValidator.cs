using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.BannerTypes.Commands.Validators
{

    public class UpdateBannerTypeCommandValidator : BannerTypeValidatorBase<UpdateBannerTypeCommand>
    {
        public UpdateBannerTypeCommandValidator(IApplicationDbContext context) : base(context)
        {
        }
    }
}
