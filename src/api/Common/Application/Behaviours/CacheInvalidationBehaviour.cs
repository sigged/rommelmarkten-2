using MediatR;
using Rommelmarkten.Api.Common.Application.Caching;
using Rommelmarkten.Api.Common.Application.Interfaces;
using System.Reflection;

namespace Rommelmarkten.Api.Common.Application.Behaviours
{
    public class CacheInvalidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ICacheManager cacheManager;

        public CacheInvalidationBehaviour(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //execute pipeline/handler first
            var task = next();

            //invalidate cache if pipeline completes successfully
            _ = task.ContinueWith(async (task) =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    var cacheInvalidatorAttributes = request.GetType().GetCustomAttributes<CacheInvalidatorAttribute>();
                    if (cacheInvalidatorAttributes.Any())
                    {
                        foreach (var cacheInvalidatorAttribute in cacheInvalidatorAttributes)
                        {
                            await cacheManager.InvalidateCacheWithTags(cancellationToken, cacheInvalidatorAttribute.Tags);
                        }
                    }
                }
            });

            return task;
        }
    }
}
