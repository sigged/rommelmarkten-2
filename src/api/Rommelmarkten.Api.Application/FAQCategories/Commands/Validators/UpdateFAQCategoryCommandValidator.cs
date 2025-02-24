using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.FAQCategories.Commands.Validators
{

    public class UpdateFAQCategoryCommandValidator : FAQCategoryValidatorBase<UpdateFAQCategoryCommand>
    {
        public UpdateFAQCategoryCommandValidator(IApplicationDbContext context) : base(context)
        {
        }
    }
}
