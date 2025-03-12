using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Domain.Entities;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;
using Rommelmarkten.Api.Features.ShoppingLists.Events;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Commands.DeleteShoppingList
{

    [Authorize]
    public class DeleteShoppingListCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteShoppingListCommandHandler : IRequestHandler<DeleteShoppingListCommand>
    {
        private readonly IShoppingListsDbContext _context;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public DeleteShoppingListCommandHandler(IShoppingListsDbContext context, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task Handle(DeleteShoppingListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ShoppingLists
                .Where(l => l.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ShoppingList), request.Id);
            }

            if (!await _resourceAuthorizationService.AuthorizeAny(entity, Policies.MustBeCreator, Policies.MustBeAdmin))
            {
                throw new ForbiddenAccessException();
            }

            _context.ShoppingLists.Remove(entity);

            entity.DomainEvents.Append(new ShoppingListRemovedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
