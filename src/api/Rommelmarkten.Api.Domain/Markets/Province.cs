using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Markets
{
    public class Province : EntityBase<Guid>
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string UrlSlug { get; set; }
        public required string Language { get; set; }
    }
}
