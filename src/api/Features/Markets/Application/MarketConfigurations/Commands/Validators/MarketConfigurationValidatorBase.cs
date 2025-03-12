using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.Markets.Application.Gateways;
using Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Models;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketConfigurations.Commands.Validators
{
    public abstract class MarketConfigurationValidatorBase<T> : AbstractValidator<T>
        where T : MarketConfigurationDto
    {
        protected readonly IMarketsDbContext _context;

        public MarketConfigurationValidatorBase(IMarketsDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("A configuration with this name already exists.");
        }

        public async Task<bool> BeUniqueName(T entity, string name, CancellationToken cancellationToken)
        {
            return !await _context.MarketConfigurations
                .AnyAsync(l => l.Name == name && l.Id != entity.Id, cancellationToken);
        }
    }
}
