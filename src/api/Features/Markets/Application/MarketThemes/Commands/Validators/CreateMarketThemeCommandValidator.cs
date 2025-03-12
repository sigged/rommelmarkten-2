using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.Markets.Application.Gateways;

namespace Rommelmarkten.Api.Features.Markets.Application.MarketThemes.Commands.Validators
{
    public class CreateMarketThemeCommandValidator : MarketThemeValidatorBase<CreateMarketThemeCommand>
    {
        public CreateMarketThemeCommandValidator(IMarketsDbContext context) : base(context)
        {
            RuleFor(v => v.Id)
                .Empty().WithMessage("Use a default Id value when creating a new entity.")
                .MustAsync(BeUniqueId).WithMessage("A configuration with this Id already exists.");
        }
        public async Task<bool> BeUniqueId(Guid id, CancellationToken cancellationToken)
        {
            return await _context.MarketThemes
                .AllAsync(l => l.Id != id);
        }
    }
}
