using Rommelmarkten.Api.Application.AffiliateAds.Commands;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;

namespace Rommelmarkten.Api.Features.Affiliates.Application.Commands.Validators
{

    public class UpdateAffiliateAdCommandValidator : AffiliateAdValidatorBase<UpdateAffiliateAdCommand>
    {
        public UpdateAffiliateAdCommandValidator(IAffiliatesDbContext context) : base(context)
        {
        }
    }
}
