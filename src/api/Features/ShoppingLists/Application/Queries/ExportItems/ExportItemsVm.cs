﻿namespace Rommelmarkten.Api.Features.ShoppingLists.Application.Queries.ExportItems
{
    public class ExportItemsVm
    {
        public required string FileName { get; set; }

        public required string ContentType { get; set; }

        public required byte[] Content { get; set; }
    }
}