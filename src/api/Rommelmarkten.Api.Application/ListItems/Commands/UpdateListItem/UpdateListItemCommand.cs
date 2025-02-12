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

namespace Rommelmarkten.Api.Application.ListItems.Commands.UpdateListItem
{

    [Authorize]
    public class UpdateListItemCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public bool Done { get; set; }
    }

    public class UpdateListItemCommandHandler : IRequestHandler<UpdateListItemCommand>
    {
        private readonly IApplicationDbContext _context; 
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public UpdateListItemCommandHandler(IApplicationDbContext context, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task Handle(UpdateListItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ListItems.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(ListItem), request.Id);
            }

            var parentList = await _context.ShoppingLists
               .Include(e => e.Associates)
               .SingleOrDefaultAsync(e => e.Id.Equals(entity.ListId));

            if (!await _resourceAuthorizationService.AuthorizeAny(parentList, Policies.MustHaveListAccess, Policies.MustBeCreator, Policies.MustBeAdmin))
            {
                throw new ForbiddenAccessException();
            }

            entity.Title = request.Title;
            entity.CategoryId = request.CategoryId;
            entity.Done = request.Done;

            entity.DomainEvents.Add(new ListItemUpdatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
