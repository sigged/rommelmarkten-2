using Rommelmarkten.Api.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Application.ShoppingLists.Commands.CreateShoppingList
{
    public class CreateShoppingListCommandValidator : AbstractValidator<CreateShoppingListCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateShoppingListCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("A category with this title already exists.");

            RuleFor(v => v.Color)
                .MaximumLength(10).WithMessage("Color cannot exceed 10 characters");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.ShoppingLists
                .AllAsync(l => l.Title != title);
        }
    }
}
