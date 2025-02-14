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

namespace Rommelmarkten.Api.Application.ListItems.Commands.CreateListItem
{

    [Authorize]
    public class CreateListItemCommand : IRequest<int>
    {
        public int ListId { get; set; }

        public required string Title { get; set; }

        public int CategoryId { get; set; }
    }

    public class CreateListItemCommandHandler : IRequestHandler<CreateListItemCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDomainEventService _domainEventService;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public CreateListItemCommandHandler(IApplicationDbContext context, IDomainEventService domainEventService, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _domainEventService = domainEventService;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task<int> Handle(CreateListItemCommand request, CancellationToken cancellationToken)
        {
            var parentList = await _context.ShoppingLists
                .Include(e => e.Associates)
                .SingleOrDefaultAsync(e => e.Id.Equals(request.ListId));
            var category = await _context.Categories.FindAsync(request.CategoryId);

            if (parentList == null)
            {
                throw new NotFoundException(nameof(ShoppingList), request.ListId);
            }
            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.CategoryId);
            }
            if (!await _resourceAuthorizationService.AuthorizeAny(parentList, Policies.MustHaveListAccess, Policies.MustBeAdmin))
            {
                throw new ForbiddenAccessException();
            }

            var entity = new ListItem
            {
                ListId = request.ListId,
                Title = request.Title,
                CategoryId = request.CategoryId,
                Done = false
            };

            _context.ListItems.Add(entity);

            entity.DomainEvents.Append(new ListItemCreatedEvent(entity));

            await _context.SaveChangesAsync(cancellationToken);


            return entity.Id;
        }
    }
}
