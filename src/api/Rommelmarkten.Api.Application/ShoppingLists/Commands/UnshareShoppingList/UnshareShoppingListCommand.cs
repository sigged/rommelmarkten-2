﻿using Rommelmarkten.Api.Application.Common.Exceptions;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Domain.Entities;
using Rommelmarkten.Api.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Application.ShoppingLists.Commands.UnshareShoppingList
{
    [Authorize]
    public class UnshareShoppingListCommand : IRequest
    {
        public int Id { get; set; }

        public required string AssociateUserName { get; set; }
    }

    public class UnshareShoppingListCommandHandler : IRequestHandler<UnshareShoppingListCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IDomainEventService _domainEventService;
        private readonly IResourceAuthorizationService _resourceAuthorizationService;

        public UnshareShoppingListCommandHandler(IApplicationDbContext context, IIdentityService identityService, IDomainEventService domainEventService, IResourceAuthorizationService resourceAuthorizationService)
        {
            _context = context;
            _identityService = identityService;
            _domainEventService = domainEventService;
            _resourceAuthorizationService = resourceAuthorizationService;
        }

        public async Task Handle(UnshareShoppingListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ShoppingLists
                .Include(e => e.Associates)
                .SingleOrDefaultAsync(e => e.Id.Equals(request.Id));

            var associate = await _identityService.FindByName(request.AssociateUserName);
            
            if (entity == null)
            {
                throw new NotFoundException(nameof(ShoppingList), request.Id);
            }
            if (associate == null)
            {
                throw new NotFoundException(nameof(IUser), request.AssociateUserName);
            }

            if (!await _resourceAuthorizationService.AuthorizeAny(entity, Policies.MustBeCreator, Policies.MustBeAdmin) &&
                !await _resourceAuthorizationService.AuthorizeAny(Tuple.Create(associate.Id, entity), Policies.MustMatchListAssociation)
                )
            {
                throw new ForbiddenAccessException();
            }

            var association = await _context.ListAssociates.FirstOrDefaultAsync(e =>
                e.AssociateId.Equals(associate.Id) &&
                e.ListId.Equals(entity.Id));
            if (association == null)
            {
                throw new NotFoundException("The list is not shared with this user");
            }

            _context.ListAssociates.Remove(association);

            entity.DomainEvents.Append(new ShoppingListShareEvent(entity, associate, false));

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
