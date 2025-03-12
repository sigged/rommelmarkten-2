using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.NewsArticles.Domain
{
    public class NewsArticle : AuditableEntity<Guid>
    {
        public required string Title { get; set; }

        public required string Content { get; set; }

        public DateTimeOffset? DisplayUntil { get; set; }
    }
}
