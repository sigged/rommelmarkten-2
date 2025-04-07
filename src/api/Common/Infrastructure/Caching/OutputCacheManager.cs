using Microsoft.AspNetCore.OutputCaching;
using Rommelmarkten.Api.Common.Application.Interfaces;

namespace Rommelmarkten.Api.Common.Infrastructure.Caching
{
    public class OutputCacheManager : ICacheManager
    {
        private readonly IOutputCacheStore cacheStore;

        public OutputCacheManager(IOutputCacheStore cacheStore)
        {
            this.cacheStore = cacheStore;
        }

        public async Task InvalidateCacheWithTags(CancellationToken cancellationToken, params string[] tags)
        {
            foreach (var tag in tags)
            {
                //invalidate cache with tag
                await cacheStore.EvictByTagAsync(tag, cancellationToken);
            }
        }
    }
}
