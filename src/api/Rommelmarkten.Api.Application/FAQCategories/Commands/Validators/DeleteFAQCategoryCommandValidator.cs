using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.FAQCategories.Commands.Validators
{
    public class DeleteFAQCategoryCommandValidator : AbstractValidator<DeleteFAQCategoryCommand>
    {
        private readonly IApplicationDbContext context;

        public DeleteFAQCategoryCommandValidator(IApplicationDbContext context) 
        {
            RuleFor(v => v.Id)
                .MustAsync(HaveNoChildItems).WithMessage("Category cannot be deleted when it still has child items");
            this.context = context;
        }

        public async Task<bool> HaveNoChildItems(Guid id, CancellationToken cancellationToken)
        {
            return !await context.FAQCategories
                .Where(e => e.Id.Equals(id))
                .AnyAsync(c => c.FAQItems.Any(), cancellationToken: cancellationToken);
        }
    }
}
