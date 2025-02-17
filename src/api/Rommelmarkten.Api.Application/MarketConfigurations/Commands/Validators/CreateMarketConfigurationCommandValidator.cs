using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Application.MarketConfigurations.Commands.Validators
{
    public class CreateMarketConfigurationCommandValidator : MarketConfigurationValidatorBase<CreateMarketConfigurationCommand>
    {
        public CreateMarketConfigurationCommandValidator(IApplicationDbContext context) : base(context)
        {
            RuleFor(v => v.Id)
                .Empty().WithMessage("Use a default Id value when creating a new entity.")
                .MustAsync(BeUniqueId).WithMessage("A configuration with this Id already exists.");
        }
        public async Task<bool> BeUniqueId(Guid id, CancellationToken cancellationToken)
        {
            return await _context.MarketConfigurations
                .AllAsync(l => l.Id != id);
        }
    }
}
