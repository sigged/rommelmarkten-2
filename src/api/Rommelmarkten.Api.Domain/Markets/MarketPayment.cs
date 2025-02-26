using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Markets
{
    public class MarketPayment : EntityBase<Guid>
    {
        public Guid MarketId { get; set; }

        public Market? Market { get; set; }

        public decimal Amount { get; set; }

        PaymentStatus Status { get; set; } = PaymentStatus.NotPaid;

        public bool IsPaid => Amount == 0 && Status == PaymentStatus.Paid;

        public DateTimeOffset StatusChanged { get; set; }
    }
}
