using FluentValidation;

namespace Rommelmarkten.Api.Application.ListItems.Commands.CreateListItem
{
    public class CreateListItemCommandValidator : AbstractValidator<CreateListItemCommand>
    {
        public CreateListItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
