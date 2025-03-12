using Rommelmarkten.Api.Domain.Entities;

namespace Rommelmarkten.Api.Features.ShoppingLists.Domain
{
    public class ListAssociate
    {
        public required string AssociateId { get; set; }

        public int ListId { get; set; }

        public ShoppingList? List { get; set; }

        public DateTime? AssociatedOn { get; set; }
    }
}
