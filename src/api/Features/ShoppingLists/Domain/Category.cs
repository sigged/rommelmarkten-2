using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Features.ShoppingLists.Domain;

namespace Rommelmarkten.Api.Domain.Entities
{
    public class Category : AuditableEntity<int>
    {
        public required string Name { get; set; }

        public required string Icon { get; set; }

        public IList<ListItem> Items { get; private set; } = new List<ListItem>();
    }
}
