using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Markets.Domain
{

    public class MarketInvoice : EntityBase<Guid>
    {
        public required string InvoiceNumber { get; set; }

        public Guid MarketId { get; set; }

        public Market? Market { get; set; }

        public decimal Amount => InvoiceLines.Sum(x => x.Amount);

        public ICollection<MarketInvoiceLine> InvoiceLines { get; set; } = [];

        public ICollection<MarketInvoiceReminder> PaymentReminders { get; set; } = [];

        public PaymentStatus Status { get; set; } = PaymentStatus.NotPaid;

        public bool IsPaid => Amount == 0 && Status == PaymentStatus.Paid;

        public DateTimeOffset StatusChanged { get; set; }

    }
}
