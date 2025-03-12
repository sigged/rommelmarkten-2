using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Models;
using Rommelmarkten.Api.Features.Markets.Application.Gateways;

namespace Rommelmarkten.Api.Features.Markets.Application.BannerTypes.Commands.Validators
{
    public abstract class BannerTypeValidatorBase<T> : AbstractValidator<T>
        where T : BannerTypeDto
    {
        protected readonly IMarketsDbContext _context;

        public BannerTypeValidatorBase(IMarketsDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("A configuration with this name already exists.");
        }

        public async Task<bool> BeUniqueName(T entity, string name, CancellationToken cancellationToken)
        {
            return !await _context.BannerTypes
                .AnyAsync(l => l.Name == name && l.Id != entity.Id, cancellationToken);
        }
    }
}
