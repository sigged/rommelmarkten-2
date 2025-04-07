using FluentValidation;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Commands.UnshareShoppingList
{
    public class UnshareShoppingListCommandValidator : AbstractValidator<UnshareShoppingListCommand>
    {
        private readonly IShoppingListsDbContext _context;

        public UnshareShoppingListCommandValidator(IShoppingListsDbContext context)
        {
            _context = context;
        }
    }
}
