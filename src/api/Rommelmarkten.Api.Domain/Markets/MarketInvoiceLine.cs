using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Markets
{
    public class MarketInvoiceLine : EntityBase<Guid>
    {
        public Guid ParentInvoiceId { get; set; }

        public MarketInvoice? ParentInvoice { get; set; }

        public required string Subject { get; set; }

        public required decimal Amount { get; set; }

    }
}
