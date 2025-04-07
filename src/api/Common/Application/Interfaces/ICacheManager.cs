namespace Rommelmarkten.Api.Common.Application.Interfaces
{
    public interface ICacheManager
    {
        Task InvalidateCacheWithTags(CancellationToken cancellationToken, params string[] tags);
    }
}
