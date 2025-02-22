using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.AffiliateAds.Commands.Validators
{

    public class UpdateAffiliateAdCommandValidator : AffiliateAdValidatorBase<UpdateAffiliateAdCommand>
    {
        public UpdateAffiliateAdCommandValidator(IApplicationDbContext context) : base(context)
        {
        }
    }
}
