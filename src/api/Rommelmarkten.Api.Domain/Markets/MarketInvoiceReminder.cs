using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Markets
{
    public class MarketInvoiceReminder : EntityBase<Guid>
    {
        public Guid ParentInvoiceId { get; set; }

        public MarketInvoice? ParentInvoice { get; set; }

        public DateTimeOffset SentDate { get; set; }
    }
}
