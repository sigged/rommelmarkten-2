namespace Rommelmarkten.Api.Application.ShoppingLists.Queries.ExportItems
{
    public class ExportItemsVm
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}