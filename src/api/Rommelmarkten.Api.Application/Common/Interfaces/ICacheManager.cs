namespace Rommelmarkten.Api.Application.Common.Interfaces
{
    public interface ICacheManager
    {
        Task InvalidateCacheWithTags(CancellationToken cancellationToken, params string[] tags);
    }
}
