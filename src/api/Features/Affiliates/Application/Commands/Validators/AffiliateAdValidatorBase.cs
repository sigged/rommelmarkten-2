using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Features.Affiliates.Application.Gateways;
using Rommelmarkten.Api.Features.Affiliates.Application.Models;

namespace Rommelmarkten.Api.Features.Affiliates.Application.Commands.Validators
{
    public abstract class AffiliateAdValidatorBase<T> : AbstractValidator<T>
        where T : AffiliateAdDto
    {
        protected readonly IAffiliatesDbContext _context;

        public AffiliateAdValidatorBase(IAffiliatesDbContext context)
        {
            _context = context;

            RuleFor(v => v.AffiliateName)
                .NotEmpty().WithMessage("AffiliateName is required.")
                .MaximumLength(200).WithMessage("AffiliateName must not exceed 200 characters.")
                .MustAsync(BeUniqueName).WithMessage("An affiliate with this name already exists.");
        }

        public async Task<bool> BeUniqueName(T entity, string name, CancellationToken cancellationToken)
        {
            return !await _context.AffiliateAds
                .AnyAsync(l => l.AffiliateName == name && l.Id != entity.Id, cancellationToken);
        }
    }
}
