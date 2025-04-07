using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.Affiliates.Application.Gateways;

namespace Rommelmarkten.Api.Features.Affiliates.Application.Commands.Validators
{
    public class CreateAffiliateAdCommandValidator : AffiliateAdValidatorBase<CreateAffiliateAdCommand>
    {
        public CreateAffiliateAdCommandValidator(IAffiliatesDbContext context) : base(context)
        {
            RuleFor(v => v.Id)
                .Empty().WithMessage("Use a default Id value when creating a new entity.")
                .MustAsync(BeUniqueId).WithMessage("A configuration with this Id already exists.");
        }
        public async Task<bool> BeUniqueId(Guid id, CancellationToken cancellationToken)
        {
            return await _context.AffiliateAds
                .AllAsync(l => l.Id != id);
        }
    }
}
