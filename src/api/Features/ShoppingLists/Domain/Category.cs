using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.ShoppingLists.Domain
{
    public class Category : AuditableEntity<int>
    {
        public required string Name { get; set; }

        public required string Icon { get; set; }

        public IList<ListItem> Items { get; private set; } = new List<ListItem>();
    }
}
