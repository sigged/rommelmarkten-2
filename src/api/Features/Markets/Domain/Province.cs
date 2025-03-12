using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Markets.Domain
{
    public class Province : EntityBase<Guid>
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public required string UrlSlug { get; set; }
        public required string Language { get; set; }

        public ICollection<Market> Markets { get; set; } = [];
    }
}
