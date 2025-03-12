using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.FAQs.Application.Gateways;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Commands.Validators
{
    public class CreateFAQCategoryCommandValidator : FAQCategoryValidatorBase<CreateFAQCategoryCommand>
    {
        public CreateFAQCategoryCommandValidator(IFAQsDbContext context) : base(context)
        {
            RuleFor(v => v.Id)
                .Empty().WithMessage("Use a default Id value when creating a new entity.")
                .MustAsync(BeUniqueId).WithMessage("A category with this Id already exists.");
        }

        public async Task<bool> BeUniqueId(Guid id, CancellationToken cancellationToken)
        {
            return await _context.FAQCategories
                .AllAsync(l => l.Id != id);
        }
    }
}
