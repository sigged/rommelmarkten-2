using Rommelmarkten.Api.Features.FAQs.Application.Gateways;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQItems.Commands.Validators
{

    public class UpdateFAQItemCommandValidator : FAQItemValidatorBase<UpdateFAQItemCommand>
    {
        public UpdateFAQItemCommandValidator(IFAQsDbContext context) : base(context)
        {
        }
    }
}
