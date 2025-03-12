using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.FAQs.Application.Gateways;

namespace Rommelmarkten.Api.Features.FAQs.Application.FAQItems.Commands.Validators
{
    public class CreateFAQItemCommandValidator : FAQItemValidatorBase<CreateFAQItemCommand>
    {
        public CreateFAQItemCommandValidator(IFAQsDbContext context) : base(context)
        {
            RuleFor(v => v.Id)
                .Empty().WithMessage("Use a default Id value when creating a new entity.")
                .MustAsync(BeUniqueId).WithMessage("A configuration with this Id already exists.");
        }
        public async Task<bool> BeUniqueId(Guid id, CancellationToken cancellationToken)
        {
            return await _context.FAQItems
                .AllAsync(l => l.Id != id);
        }
    }
}
