using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.BannerTypes.Models;

namespace Rommelmarkten.Api.Application.BannerTypes.Commands.Validators
{
    public abstract class BannerTypeValidatorBase<T> : AbstractValidator<T>
        where T : BannerTypeDto
    {
        protected readonly IApplicationDbContext _context;

        public BannerTypeValidatorBase(IApplicationDbContext context)
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
