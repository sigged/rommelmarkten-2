using Rommelmarkten.Api.Common.Application.Mappings;
using Rommelmarkten.Api.Features.NewsArticles.Domain;

namespace Rommelmarkten.Api.Features.NewsArticles.Application.Models
{
    public class NewsArticleDto : IMapFrom<NewsArticle>
    {
        public Guid Id { get; set; }

        public required string Title { get; set; }

        public required string Content { get; set; }

        public DateTimeOffset? DisplayUntil { get; set; }
    }
}
