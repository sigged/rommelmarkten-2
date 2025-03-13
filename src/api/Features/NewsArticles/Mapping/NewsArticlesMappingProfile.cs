using Rommelmarkten.Api.Common.Application.Mappings;

namespace Rommelmarkten.Api.Features.NewsArticles.Mapping
{

    /// <summary>
    /// Wires up the IMapFrom<T> implementations in this assembly when scanning the assembly for Profiles
    /// </summary>
    public class NewsArticlesMappingProfile : DiscoverIMapFromProfile
    {
        public NewsArticlesMappingProfile()
            : base()
        {

        }
    }
}