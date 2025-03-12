using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Application.Models;
using Rommelmarkten.Api.Common.Application.Security;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Gateways;
using Rommelmarkten.Api.Features.ShoppingLists.Application.Queries.GetShoppingLists;
using Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.Models;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime
{

    [Authorize]
    public class FetchHub : Hub
    {
        private readonly IShoppingListsDbContext _dbContext;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly ClientProxyMethods _clientProxyMethods;

        private const string shoppingListChannelPrefix = "shoppinglist/";

        public FetchHub(IShoppingListsDbContext dbContext, IIdentityService identityService, IMapper mapper)
        {
            _dbContext = dbContext;
            _identityService = identityService;
            _mapper = mapper;
            _clientProxyMethods = new ClientProxyMethods(this);
        }

        public override async Task OnConnectedAsync()
        {
            if (Context.UserIdentifier == null)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                var listsForUser = await _dbContext.ShoppingLists
                .Where(e => e.CreatedBy != null && e.CreatedBy.Equals(Context.UserIdentifier) ||
                            e.Associates.Select(a => a.AssociateId).Contains(Context.UserIdentifier))
                .ToListAsync();

                var userName = await _identityService.GetUserNameAsync(Context.UserIdentifier);
                var user = await _identityService.FindByName(userName);

                foreach (var shoppingList in listsForUser)
                {
                    string groupName = FetchHubHelpers.GetShoppingListGroupName(shoppingList.Id.ToString());
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

                    var args = new UserJoinedGroupDto(
                        _mapper.Map<ShoppingListDto>(shoppingList),
                        _mapper.Map<UserDto>(user));
                    await _clientProxyMethods.OnUserJoinedGroup(groupName, args);
                }
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (Context.UserIdentifier == null)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                var listsForUser = await _dbContext.ShoppingLists
                .Where(e => e.CreatedBy != null && e.CreatedBy.Equals(Context.UserIdentifier) ||
                            e.Associates.Select(a => a.AssociateId).Contains(Context.UserIdentifier))
                .ToListAsync();

                var userName = await _identityService.GetUserNameAsync(Context.UserIdentifier);
                var user = await _identityService.FindByName(userName);

                foreach (var shoppingList in listsForUser)
                {
                    string groupName = FetchHubHelpers.GetShoppingListGroupName(shoppingList.Id.ToString());
                    await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

                    var args = new UserLeftGroupDto(
                            _mapper.Map<ShoppingListDto>(shoppingList),
                            _mapper.Map<UserDto>(user));
                    await _clientProxyMethods.OnUserLeftGroup(groupName, args);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }

    }
}
