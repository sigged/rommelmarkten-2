using FluentValidation;

namespace Rommelmarkten.Api.Common.Application.Pagination
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
