using FluentValidation;

namespace Rommelmarkten.Api.Application.ListItems.Commands.UpdateListItem
{
    public class UpdateListItemCommandValidator : AbstractValidator<UpdateListItemCommand>
    {
        public UpdateListItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
