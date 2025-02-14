using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Entities;
using Rommelmarkten.Api.Domain.Events;
using Rommelmarkten.Api.Domain.ValueObjects;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Application.ShoppingLists.Commands.CreateShoppingList
{

    [Authorize]
    public class CreateShoppingListCommand : IRequest<int>
    {
        public required string Title { get; set; }
        public required string Color { get; set; }
    }

    public class CreateShoppingListCommandHandler : IRequestHandler<CreateShoppingListCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateShoppingListCommandHandler(IApplicationDbContext context)
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
