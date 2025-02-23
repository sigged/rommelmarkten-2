using Rommelmarkten.Api.Application.Common.Mappings;
using Rommelmarkten.Api.Domain.Content;

namespace Rommelmarkten.Api.Application.NewsArticles.Models
{
    public class NewsArticleDto : IMapFrom<NewsArticle>
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Content { get; set; }

        public DateTimeOffset? DisplayUntil { get; set; }
    }
}
