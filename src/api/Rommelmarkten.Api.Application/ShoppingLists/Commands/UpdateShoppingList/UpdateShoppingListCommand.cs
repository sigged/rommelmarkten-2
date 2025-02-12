using Rommelmarkten.Api.Application.Common.Exceptions;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Entities;
using Rommelmarkten.Api.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Application.ShoppingLists.Commands.UpdateShoppingList
{
    [Authorize]
    public class UpdateShoppingListCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
    }

    public class UpdateShoppingListCommandHandler : IRequestHandler<UpdateShoppingListCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public UpdateShoppingListCommandHandler(IApplicationDbContext context, IResourceAuthorizationService resourceAuthorizationService)
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
