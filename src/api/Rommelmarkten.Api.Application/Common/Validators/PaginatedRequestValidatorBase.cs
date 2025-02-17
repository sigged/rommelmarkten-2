using FluentValidation;
using Rommelmarkten.Api.Application.Common.Models;

namespace Rommelmarkten.Api.Application.Common.Validators
{
    public abstract class PaginatedRequestValidatorBase<T> : AbstractValidator<T>
        where T : PaginatedRequest
    {

        public PaginatedRequestValidatorBase()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}
