using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Markets
{

    public class Organizer
    {
        public required string Name { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public string? URL { get; set; }
        public string? ContactNotes { get; set; }

        public bool ShowName { get; set; }

        public bool ShowPhone { get; set; }

        public bool ShowEmail { get; set; }

    }
    public class MarketImage
    {
        public required string ImageUrl { get; set; }
        public required string ImageThumbUrl { get; set; }
    }

    public class MarketLocation
    {
        public string? Hall { get; set; }

        public required string Address { get; set; }

        public required string PostalCode { get; set; }

        public required string City { get; set; }

        public required string Country { get; set; }

        public double? CoordLatitude { get; set; }

        public double? CoordLongitude { get; set; }

    }
    public class MarketPricing
    {
        public decimal EntryPrice { get; set; }

        public decimal StandPrice { get; set; }

    }

    public class Market : AuditableEntity<Guid>
    {
        /// <summary>
        /// Legacy ID from v1 database
        /// </summary>
        public long? Id_V1 { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public Guid ConfigurationId { get; set; }

        public MarketConfiguration? Configuration { get; set; }

        public Guid BannerTypeId { get; set; }

        public BannerType? BannerType { get; set; }

        public Guid ProvinceId { get; set; }

        public Province? Province { get; set; }

        public ICollection<MarketTheme> Themes { get; set; } = [];

        public ICollection<MarketDate> Dates { get; set; } = [];

        //public ICollection<MarketRevision> Revisions { get; set; } = [];

        public required MarketPricing Pricing { get; set; }

        public required MarketLocation Location { get; set; }

        public required MarketImage Image { get; set; }

        public required Organizer Organizer { get; set; }

        public DateTime? DateLastAdminUpdate { get; set; }

        public DateTime? DateLastUserUpdate { get; set; }

        /// <summary>
        /// true if approved for display
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// pause function (for customer)
        /// </summary>
        public bool IsSuspended { get; set; }

        public bool IsNotSuspended => !IsSuspended;

        public bool IsExpired()
        {
            return Dates.All(date => date.Date < DateOnly.FromDateTime(DateTime.Today));
        }

        //public PaymentStatus CheckPaymentStatus()
        //{
        //    if (Sales == null)
        //    {
        //        throw new ArgumentNullException("Sales property not loaded!");
        //    }
        //    else
        //    {
        //        if (Sales.Count == 0)
        //        {
        //            return PaymentStatus.Unknown;
        //        }
        //        else
        //        {
        //            decimal totalprice = Sales.Sum(s => s.TotalPrice);
        //            decimal unpaidprice = Sales.Where(e => !e.IsPaid).Sum(s => s.TotalPrice); //to illustrate diffent prices with this shit multi-payment system which is not even needed
        //            if (totalprice == 0)
        //            {
        //                return PaymentStatus.Free;
        //            }
        //            else
        //            {
        //                if (Sales.All(s => s.IsPaid))
        //                {
        //                    return PaymentStatus.Paid;
        //                }
        //                else
        //                {
        //                    return PaymentStatus.NotPaid;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
