namespace Rommelmarkten.Api.Domain.Entities
{
    public class ListAssociate
    {
        public required string AssociateId { get; set; }

        public int ListId { get; set; }

        public ShoppingList? List { get; set; }

        public DateTime? AssociatedOn { get; set; }
    }
}
