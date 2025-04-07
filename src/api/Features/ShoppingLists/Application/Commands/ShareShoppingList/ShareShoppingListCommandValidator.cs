using FluentValidation;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Commands.ShareShoppingList
{
    public class ShareShoppingListCommandValidator : AbstractValidator<ShareShoppingListCommand>
    {
        private readonly IShoppingListsDbContext _context;

        public ShareShoppingListCommandValidator(IShoppingListsDbContext context)
        {
            _context = context;
        }
    }
}
