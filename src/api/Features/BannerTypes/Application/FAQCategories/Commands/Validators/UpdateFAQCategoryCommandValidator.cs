using Rommelmarkten.Api.Features.FAQs.Application.Gateways;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Commands.Validators
{

    public class UpdateFAQCategoryCommandValidator : FAQCategoryValidatorBase<UpdateFAQCategoryCommand>
    {
        public UpdateFAQCategoryCommandValidator(IFAQsDbContext context) : base(context)
        {
        }
    }
}
