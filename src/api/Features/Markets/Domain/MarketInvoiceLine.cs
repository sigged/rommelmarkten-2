using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Markets.Domain
{
    public class MarketInvoiceLine : EntityBase<Guid>
    {
        public Guid ParentInvoiceId { get; set; }

        public MarketInvoice? ParentInvoice { get; set; }

        public required string Subject { get; set; }

        public required decimal Amount { get; set; }

    }
}
