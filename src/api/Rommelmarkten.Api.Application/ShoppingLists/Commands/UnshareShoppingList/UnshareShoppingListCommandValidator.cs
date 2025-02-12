using Rommelmarkten.Api.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using Rommelmarkten.Api.Application.ShoppingLists.Commands.ShareShoppingList;

namespace Rommelmarkten.Api.Application.ShoppingLists.Commands.UnshareShoppingList
{
    public class UnshareShoppingListCommandValidator : AbstractValidator<UnshareShoppingListCommand>
    {
        private readonly IApplicationDbContext _context;

        public UnshareShoppingListCommandValidator(IApplicationDbContext context)
        {
            _context = context;
        }
    }
}
