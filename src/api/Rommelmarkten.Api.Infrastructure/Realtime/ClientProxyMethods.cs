using Microsoft.AspNetCore.SignalR;
using Rommelmarkten.Api.Infrastructure.Realtime.Models;

namespace Rommelmarkten.Api.Infrastructure.Realtime
{
    internal class ClientProxyMethods
    {
        protected IHubContext<FetchHub> _hubContext;
        protected FetchHub _hub;

        public ClientProxyMethods(IHubContext<FetchHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public ClientProxyMethods(FetchHub hub)
        {
            _hub = hub;
        }

#nullable enable
        protected Func<string, object?, CancellationToken, Task> ClientGroupSendAsync(string groupName)
#nullable restore
        {
            if (_hub != null)
                return _hub.Clients.Groups(groupName).SendAsync;
            else if (_hubContext != null)
                return _hubContext.Clients.Groups(groupName).SendAsync;
            else
                throw new InvalidOperationException("Uninitialized Hub or HubContext");
        }

        public Task OnListItemCreated(string recipientGroup, ListItemCreatedEventDto args, CancellationToken cancellationToken = default)
            => ClientGroupSendAsync(recipientGroup)
                (nameof(OnListItemCreated), args, cancellationToken);

        public Task OnListItemUpdated(string recipientGroup, ListItemUpdatedEventDto args, CancellationToken cancellationToken = default)
            => ClientGroupSendAsync(recipientGroup)
                (nameof(OnListItemUpdated), args, cancellationToken);

        public Task OnListItemRemoved(string recipientGroup, ListItemRemovedEventDto args, CancellationToken cancellationToken = default)
            => ClientGroupSendAsync(recipientGroup)
                (nameof(OnListItemRemoved), args, cancellationToken);

        public Task OnListCreated(string recipientGroup, ShoppingListCreatedEventDto args, CancellationToken cancellationToken = default)
            => ClientGroupSendAsync(recipientGroup)
                (nameof(OnListCreated), args, cancellationToken);

        public Task OnListUpdated(string recipientGroup, ShoppingListUpdatedEventDto args, CancellationToken cancellationToken = default)
            => ClientGroupSendAsync(recipientGroup)
                (nameof(OnListUpdated), args, cancellationToken);

        public Task OnListRemoved(string recipientGroup, ShoppingListRemovedEventDto args, CancellationToken cancellationToken = default)
            => ClientGroupSendAsync(recipientGroup)
                (nameof(OnListRemoved), args, cancellationToken);

        public Task OnListShared(string recipientGroup, ShoppingListShareEventDto args, CancellationToken cancellationToken = default)
            => ClientGroupSendAsync(recipientGroup)
                (nameof(OnListShared), args, cancellationToken);

        public Task OnUserJoinedGroup(string recipientGroup, UserJoinedGroupDto args, CancellationToken cancellationToken = default)
            => ClientGroupSendAsync(recipientGroup)
                (nameof(OnUserJoinedGroup), args, cancellationToken);

        public Task OnUserLeftGroup(string recipientGroup, UserLeftGroupDto args, CancellationToken cancellationToken = default)
            => ClientGroupSendAsync(recipientGroup)
                (nameof(OnUserLeftGroup), args, cancellationToken);
    }
}
