using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime
{
    public static class IEndpointRouteBuilderExtensions
    {
        public static void MapRealtimeMessaging(this IEndpointRouteBuilder endpoints, string url)
        {
            if (endpoints == null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }

            endpoints.MapHub<FetchHub>(url);
        }
    }
}
