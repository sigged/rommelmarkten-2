using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Content
{
    public class NewsArticle : AuditableEntity<Guid>
    {
        public required string Title { get; set; }

        public required string Content { get; set; }

        public DateTimeOffset? DisplayUntil { get; set; }
    }
}
