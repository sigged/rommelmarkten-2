using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.FAQItems.Models;

namespace Rommelmarkten.Api.Application.FAQItems.Commands.Validators
{
    public abstract class FAQItemValidatorBase<T> : AbstractValidator<T>
        where T : FAQItemDto
    {
        protected readonly IApplicationDbContext _context;

        public FAQItemValidatorBase(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Question)
                .NotEmpty().WithMessage("Question is required.")
                .MaximumLength(200).WithMessage("Question must not exceed 200 characters.");

            RuleFor(v => v.Answer)
                .NotEmpty().WithMessage("Answer is required.")
                .MaximumLength(4000).WithMessage("Answer must not exceed 4000 characters.");

            RuleFor(v => v.CategoryId)
                .MustAsync(BeExistingCategory).WithMessage(e => $"Category with Id '{e.CategoryId}' does not exist.");
        }

        public async Task<bool> BeExistingCategory(T entity, Guid categoryId, CancellationToken cancellationToken)
        {
            return await _context.FAQCategories
                .AnyAsync(e => e.Id.Equals(categoryId), cancellationToken);
        }
    }
}
