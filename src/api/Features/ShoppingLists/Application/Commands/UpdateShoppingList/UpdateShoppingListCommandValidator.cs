using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Commands.UpdateShoppingList
{
    public class UpdateShoppingListCommandValidator : AbstractValidator<UpdateShoppingListCommand>
    {
        private readonly IShoppingListsDbContext _context;

        public UpdateShoppingListCommandValidator(IShoppingListsDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");

            RuleFor(v => v.Color)
                .MaximumLength(10).WithMessage("Color cannot exceed 10 characters");
        }

        public async Task<bool> BeUniqueTitle(UpdateShoppingListCommand model, string title, CancellationToken cancellationToken)
        {
            return await _context.ShoppingLists
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Title != title);
        }
    }
}
