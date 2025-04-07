using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Models;
using Rommelmarkten.Api.Features.FAQs.Application.Gateways;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQCategories.Commands.Validators
{
    public abstract class FAQCategoryValidatorBase<T> : AbstractValidator<T>
        where T : FAQCategoryDto
    {
        protected readonly IFAQsDbContext _context;

        public FAQCategoryValidatorBase(IFAQsDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("A configuration with this name already exists.");
        }

        public async Task<bool> BeUniqueName(T entity, string name, CancellationToken cancellationToken)
        {
            return !await _context.FAQCategories
                .AnyAsync(l => l.Name == name && l.Id != entity.Id, cancellationToken);
        }
    }
}
