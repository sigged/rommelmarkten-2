using MediatR;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;
using Rommelmarkten.Api.Features.ShoppingLists.Events;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Commands.CreateShoppingList
{

    [Authorize]
    public class CreateShoppingListCommand : IRequest<int>
    {
        public required string Title { get; set; }
        public required string Color { get; set; }
    }

    public class CreateShoppingListCommandHandler : IRequestHandler<CreateShoppingListCommand, int>
    {
        private readonly IShoppingListsDbContext _context;

        public CreateShoppingListCommandHandler(IShoppingListsDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
        {
            var entity = new ShoppingList();

            entity.Title = request.Title;
            entity.Color = request.Color;

            _context.ShoppingLists.Add(entity);

            entity.DomainEvents.Append(new ShoppingListCreatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);


            return entity.Id;
        }
    }
}
