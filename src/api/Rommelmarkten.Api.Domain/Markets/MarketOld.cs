//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Net;
//using System.Security.Claims;
//using System.Security.Principal;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;
//using System.Xml.Serialization;

//namespace Rommelmarkten.Api.Domain.Markets
//{/// <summary>
// /// Extends default ASP.NET IdenityUser with core CMS account properties.
// /// </summary>
//    public abstract class Account : IdentityUser
//    {
//        public Account()
//        {
//        }

//        //public virtual async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Account> manager)
//        //{
//        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//        //    var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//        //    // Add custom user claims here
//        //    userIdentity.AddClaim(new Claim(ClaimTypes.GivenName, this.DisplayName));

//        //    return userIdentity;
//        //}

//        public abstract string TypeSystemName { get; }
//        public abstract string TypeFriendlyName { get; }
//        public abstract string TypeDescription { get; }

//       // public abstract ModelDriverBase ViewModelDriver { get; }

//        //public Guid Id { get; set; }

//        public virtual string DisplayName
//        {
//            get
//            {
//                return UserName;
//            }
//        }

//        //public string UserName { get; set; }
//        //public string Email { get; set; }
//        //public string PasswordHash { get; set; }
//        public bool IsBlocked { get; set; }
//        public bool SendMail { get; set; }

//        /// <summary>
//        /// The date when<br />
//        /// 1) the user activation mail was sent (if EmailConfirmed = false)<br />
//        /// 2) the account was activated (if EmailConfirmed = true)
//        /// </summary>
//        public DateTime? ActivationDate { get; set; }
//        public DateTime? LastActivityDate { get; set; }
//        public DateTime? LastPasswordResetDate { get; set; }
//        public int PasswordResetCount { get; set; }
//        //public string PasswordResetKey { get; set; }
//        //public bool EmailConfirmed { get; set; }
//        //public string UserActivateKey { get; set; }


//        ///// <summary>
//        ///// Returns a list of all permissions for this user, 
//        ///// by flattening the assigned roles and each role's associated permissions
//        ///// </summary>
//        //public RolePermissions GetPermissions(CmsContext c) 
//        //{
//        //    if(Roles != null){


//        //    }
//        //    return RolePermissions.None;
//        //}
//    }

//    public class MarketUser : Account
//    {
//        public override string TypeSystemName
//        {
//            get { return "RMUser"; }
//        }

//        public override string TypeFriendlyName
//        {
//            get { return "RM User"; }
//        }

//        public override string TypeDescription
//        {
//            get { return "User account for rommelmarkten"; }
//        }

//        //public override ModelDriverBase ViewModelDriver
//        //{
//        //    get
//        //    {
//        //        return new RMUserModelDriver();
//        //    }
//        //}

//        /// <summary>
//        /// Display either the name or, if latter not present, the username (=email)
//        /// </summary>
//        public override string DisplayName
//        {
//            get
//            {
//                if (!string.IsNullOrWhiteSpace(this.Name))
//                {
//                    return this.Name;
//                }
//                else
//                {
//                    return this.UserName;
//                }
//            }
//        }

//        public string Name { get; set; }
//        public string Address { get; set; }
//        public string PostalCode { get; set; }
//        public string City { get; set; }
//        public string Country { get; set; }
//        //public string Phone { get; set; } //already held in IdentityUser
//        public string VAT { get; set; }

//        //public string UserValidationRequest { get; set; }
//        //public bool IsValidated { get; set; }

//        public int? ActivationRemindersSent { get; set; }
//        public DateTime? LastActivationMailSendDate { get; set; }

//        public int? OLD_UserRights { get; set; }
//        public long? OLD_RMUserID { get; set; }
//        public string OLD_PasswordMD5Hash { get; set; }


//    }

//    public class Theme
//    {
//        //[JsonIgnore]
//        //[XmlIgnore]
//        //[ScriptIgnore]
//        public ICollection<Market> Markets { get; set; }

//        public long RMThemeId { get; set; }

//        public string Name { get; set; }

//        public string Description { get; set; }

//        public string ImageUrl { get; set; }

//        public bool IsDefault { get; set; }

//        public bool IsActive { get; set; }



//    }
//    public class Market
//    {
//        public long RMid { get; set; }

//        public long? RMTemp_Original_RMid { get; set; }

//        public Market RMTemp_Original { get; set; }

//        public Category Category { get; set; }
//        public BannerType BannerType { get; set; }
//        public ICollection<Theme> Themes { get; set; }

//        public Theme MainTheme { get; set; }

//        public ICollection<MarketDate> Dates { get; set; }

//        public MarketUser User { get; set; }

//        //leave as collection, maybe there will be future scenario's for sales (credit, debt)
//        public ICollection<MarketUser> Sales { get; set; }

//        //[Index]
//        public string Province_Code { get; set; }

//        public Province Province { get; set; }

//        public string Title { get; set; }

//        public string Description { get; set; }

//        public decimal EntryPrice { get; set; }

//        public decimal StandPrice { get; set; }

//        public string Hall { get; set; }

//        public string Address { get; set; }

//        public string PostalCode { get; set; }

//        public string City { get; set; }

//        public string Country { get; set; }

//        public string CoordLatitude { get; set; }

//        public string CoordLongitude { get; set; }

//        public DateTime DateCreated { get; set; }

//        public DateTime? DateLastAdminUpdate { get; set; }

//        public DateTime? DateLastUserUpdate { get; set; }

//        public string Image { get; set; }

//        public string ImageThumb { get; set; }

//        public string OrganizerName { get; set; }

//        public bool ShowOrganizerName { get; set; }

//        public string OrganizerPhone { get; set; }

//        public bool ShowOrganizerPhone { get; set; }

//        public string OrganizerEmail { get; set; }

//        public bool ShowOrganizerEmail { get; set; }

//        public string OrganizerURL { get; set; }

//        public string OrganizerContactNotes { get; set; }

//        /// <summary>
//        /// true if approved for display
//        /// </summary>
//        public bool IsActive { get; set; }

//        /// <summary>
//        /// pause function (for customer)
//        /// </summary>
//        public bool IsSuspended { get; set; }

//        /// <summary>
//        /// inverse wrapper around pause prop for "visible" checkbox
//        /// </summary>
//        public bool IsNotSuspended
//        {
//            get
//            {
//                return !IsSuspended;
//            }
//            set
//            {
//                IsSuspended = !value;
//            }
//        }

//        public string OLD_InOrOut { get; set; }

//        /// <summary>
//        /// used for correct migration of old db
//        /// </summary>
//        public long OLD_RMId { get; set; }

//        public bool IsExpired()
//        {
//            return Dates.All(date => date.Date < DateTime.Today);
//        }

//        //public PaymentStatus CheckPaymentStatus()
//        //{
//        //    if (Sales == null)
//        //    {
//        //        throw new ArgumentNullException("Sales property not loaded!");
//        //    }
//        //    else
//        //    {
//        //        if (Sales.Count == 0)
//        //        {
//        //            return PaymentStatus.Unknown;
//        //        }
//        //        else
//        //        {
//        //            decimal totalprice = Sales.Sum(s => s.TotalPrice);
//        //            decimal unpaidprice = Sales.Where(e => !e.IsPaid).Sum(s => s.TotalPrice); //to illustrate diffent prices with this shit multi-payment system which is not even needed
//        //            if (totalprice == 0)
//        //            {
//        //                return PaymentStatus.Free;
//        //            }
//        //            else
//        //            {
//        //                if (Sales.All(s => s.IsPaid))
//        //                {
//        //                    return PaymentStatus.Paid;
//        //                }
//        //                else
//        //                {
//        //                    return PaymentStatus.NotPaid;
//        //                }
//        //            }
//        //        }
//        //    }
//        //}
//    }

//    public class Province
//    {
//        public string Code { get; set; }
//        public string Name { get; set; }
//        public string UrlSlug { get; set; }
//        public string Language { get; set; }

//        public ICollection<Market> Markets { get; set; }
//    }

//    public class MarketDate
//    {
//        public long RMDateId { get; set; }

//        //[JsonIgnore]
//        //[XmlIgnore]
//        //[ScriptIgnore]
//        public Market Market { get; set; }

//        //public short Day { get; set; }

//        //public short Month { get; set; }

//        //public short Year { get; set; }

//        public DateTime Date { get; set; }

//        public short StartHour { get; set; }

//        public short StartMinutes { get; set; }

//        public short StopHour { get; set; }

//        public short StopMinutes { get; set; }

//        public bool IsActive { get; set; }
//    }

//    public class Category
//    {
//        public long RMCategoryID { get; set; }

//        public string Name { get; set; }

//        public string Description { get; set; }

//        public decimal Price { get; set; }

//        public bool IsActive { get; set; }

//        public int MaximumThemes { get; set; }

//        public int MaxCharacters { get; set; }

//        public bool AllowBanners { get; set; }

//        public bool AllowPoster { get; set; }

//    }

//    public class BannerType
//    {
//        public long RMBannerTypeID { get; set; }

//        public string Name { get; set; }

//        public string Description { get; set; }

//        public decimal Price { get; set; }

//        public bool IsActive { get; set; }
//    }
//}
