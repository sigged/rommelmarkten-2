using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Realtime
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
