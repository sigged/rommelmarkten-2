using Microsoft.AspNetCore.OutputCaching;
using Rommelmarkten.Api.Application.Common.Interfaces;

namespace Rommelmarkten.Api.Infrastructure.Caching
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
