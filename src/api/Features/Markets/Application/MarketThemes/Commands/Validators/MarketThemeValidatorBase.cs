using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.Markets.Application.Gateways;
using Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Models;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Commands.Validators
{
    public abstract class MarketThemeValidatorBase<T> : AbstractValidator<T>
        where T : MarketThemeDto
    {
        protected readonly IMarketsDbContext _context;

        public MarketThemeValidatorBase(IMarketsDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("A configuration with this name already exists.");
        }

        public async Task<bool> BeUniqueName(T entity, string name, CancellationToken cancellationToken)
        {
            return !await _context.MarketThemes
                .AnyAsync(l => l.Name == name && l.Id != entity.Id, cancellationToken);
        }
    }
}
