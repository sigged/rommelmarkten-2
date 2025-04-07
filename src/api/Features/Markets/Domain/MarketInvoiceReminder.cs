using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Markets.Domain
{
    public class MarketInvoiceReminder : EntityBase<Guid>
    {
        public Guid ParentInvoiceId { get; set; }

        public MarketInvoice? ParentInvoice { get; set; }

        public DateTimeOffset SentDate { get; set; }
    }
}
