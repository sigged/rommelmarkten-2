using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.FAQItems.Commands.Validators
{

    public class UpdateFAQItemCommandValidator : FAQItemValidatorBase<UpdateFAQItemCommand>
    {
        public UpdateFAQItemCommandValidator(IApplicationDbContext context) : base(context)
        {
        }
    }
}
