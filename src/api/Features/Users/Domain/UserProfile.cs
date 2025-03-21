using Rommelmarkten.Api.Common.Domain;

namespace Rommelmarkten.Api.Features.Users.Domain
{
    public class UserProfile : IAuditable, IOwnable
    {
        public required string OwnedBy { get; set; }
        //public required string UserId { get; set; }
        public bool Consented { get; set; }

        public bool IsBanned { get; set; }
        //public required Blob Avatar { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? VAT { get; set; }
        public int ActivationRemindersSent { get; set; }
        public DateTimeOffset? LastActivationMailSendDate { get; set; }

        /// <summary>
        /// The date when<br />
        /// 1) the user activation mail was sent (if EmailConfirmed = false)<br />
        /// 2) the account was activated (if EmailConfirmed = true)
        /// </summary>
        public DateTimeOffset? ActivationDate { get; set; }
        public DateTimeOffset? LastActivityDate { get; set; }
        public DateTimeOffset? LastPasswordResetDate { get; set; }
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
