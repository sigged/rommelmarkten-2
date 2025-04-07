using Microsoft.AspNetCore.SignalR;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime
{

    internal static class FetchHubClientExtensions
    {
        public static ClientProxyMethods ClientMethods(this IHubContext<FetchHub> hubContext)
            => new ClientProxyMethods(hubContext);
    }

}
