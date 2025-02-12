using AutoMapper;
using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Application.Common.Models;
using Rommelmarkten.Api.Application.Common.Security;
using Rommelmarkten.Api.Application.ShoppingLists.Queries.GetShoppingLists;
using Rommelmarkten.Api.Infrastructure.Realtime.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Realtime
{

    [Authorize]
    public class FetchHub : Hub 
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly ClientProxyMethods _clientProxyMethods;

        private const string shoppingListChannelPrefix = "shoppinglist/";

        public FetchHub(IApplicationDbContext dbContext, IIdentityService identityService, IMapper mapper)
        {
            _dbContext = dbContext;
            _identityService = identityService;
            _mapper = mapper;
            _clientProxyMethods = new ClientProxyMethods(this);
        }

        public override async Task OnConnectedAsync()
        {
            var listsForUser = await _dbContext.ShoppingLists
                .Where(e => e.CreatedBy.Equals(Context.UserIdentifier) ||
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

            await base.OnConnectedAsync();
        }
        
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var listsForUser = await _dbContext.ShoppingLists
                .Where(e => e.CreatedBy.Equals(Context.UserIdentifier) ||
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

            await base.OnDisconnectedAsync(exception);
        }

    }
}
