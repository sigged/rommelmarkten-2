using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Markets.Domain
{
    public class MarketDate : EntityBase<Guid>
    {
        public Guid ParentMarketId { get; set; }

        public Market? ParentMarket { get; set; }

        public bool IsActive { get; set; }

        public DateOnly Date { get; set; }

        public short StartHour { get; set; }

        public short StartMinutes { get; set; }

        public short StopHour { get; set; }

        public short StopMinutes { get; set; }
    }
}
