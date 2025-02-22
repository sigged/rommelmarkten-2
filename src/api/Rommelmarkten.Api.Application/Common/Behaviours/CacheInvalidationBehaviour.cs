using MediatR;
using Rommelmarkten.Api.Application.Common.Caching;
using Rommelmarkten.Api.Application.Common.Interfaces;
using System.Reflection;

namespace Rommelmarkten.Api.Application.Common.Behaviours
{
    public class CacheInvalidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ICacheManager cacheManager;

        public CacheInvalidationBehaviour(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //execute pipeline/handler first
            var response = await next();

            //invalidate cache
            var cacheInvalidatorAttributes = request.GetType().GetCustomAttributes<CacheInvalidatorAttribute>();
            if (cacheInvalidatorAttributes.Any())
            {
                foreach (var cacheInvalidatorAttribute in cacheInvalidatorAttributes)
                {
                    await cacheManager.InvalidateCacheWithTags(cancellationToken, cacheInvalidatorAttribute.Tags);
                }
            }

            return response;
        }
    }
}
