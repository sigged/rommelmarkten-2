namespace Rommelmarkten.Api.Features.Markets.Domain
{
    public class MarketWithTheme
    {
        public Guid MarketId { get; set; }
        public Market? Market { get; set; }

        public Guid ThemeId { get; set; }
        public MarketTheme? Theme { get; set; }
        public bool IsDefault { get; set; }
    }

    public class MarketRevisionWithTheme
    {
        public Guid MarketRevisionId { get; set; }
        public MarketRevision? MarketRevision { get; set; }

        public Guid ThemeId { get; set; }
        public MarketTheme? Theme { get; set; }
        public bool IsDefault { get; set; }
    }

}
