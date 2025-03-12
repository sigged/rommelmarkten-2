using MediatR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Domain.Entities;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;
using Rommelmarkten.Api.Features.ShoppingLists.Events;

namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Commands.UpdateShoppingList
{
    [Authorize]
    public class UpdateShoppingListCommand : IRequest
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Color { get; set; }
    }

    public class UpdateShoppingListCommandHandler : IRequestHandler<UpdateShoppingListCommand>
    {
        private readonly IShoppingListsDbContext _context;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public UpdateShoppingListCommandHandler(IShoppingListsDbContext context, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task Handle(UpdateShoppingListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ShoppingLists
                .Include(e => e.Associates)
                .SingleOrDefaultAsync(e => e.Id.Equals(request.Id));

            if (entity == null)
            {
                throw new NotFoundException(nameof(ShoppingList), request.Id);
            }

            if (!await _resourceAuthorizationService.AuthorizeAny(entity, Policies.MustHaveListAccess, Policies.MustBeCreator, Policies.MustBeAdmin))
            {
                throw new ForbiddenAccessException();
            }

            entity.Title = request.Title;
            entity.Color = request.Color;

            entity.DomainEvents.Append(new ShoppingListUpdatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
