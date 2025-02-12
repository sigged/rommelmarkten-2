using Rommelmarkten.Api.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Rommelmarkten.Api.Application.ShoppingLists.Commands.UpdateShoppingList;
using System;

namespace Rommelmarkten.Api.Application.ShoppingLists.Commands.ShareShoppingList
{
    public class ShareShoppingListCommandValidator : AbstractValidator<ShareShoppingListCommand>
    {
        private readonly IApplicationDbContext _context;

        public ShareShoppingListCommandValidator(IApplicationDbContext context)
        {
            _context = context;
        }
    }
}
