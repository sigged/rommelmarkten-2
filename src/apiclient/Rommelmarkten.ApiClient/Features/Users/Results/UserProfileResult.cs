namespace Rommelmarkten.ApiClient.Features.Users
{

    public partial class UsersClient
    {

        public class UserProfileResult
        {
            public string? Id { get; set; }

            public string? Email { get; set; }

            public string? UserName { get; set; }
            public bool? Consented { get; set; }

            public bool? IsBanned { get; set; }

            public string? Name { get; set; }

            public string? Address { get; set; }

            public string? PostalCode { get; set; }

            public string? City { get; set; }

            public string? Country { get; set; }

            public string? Vat { get; set; }

            public int? ActivationRemindersSent { get; set; }

            public DateTimeOffset? LastActivationMailSendDate { get; set; }

            public DateTimeOffset? ActivationDate { get; set; }

            public DateTimeOffset? LastActivityDate { get; set; }

            public DateTimeOffset? LastPasswordResetDate { get; set; }

            public DateTimeOffset? Created { get; set; }

            public string? CreatedBy { get; set; }

            public DateTimeOffset? LastModified { get; set; }

            public string? LastModifiedBy { get; set; }

        }
    }
}
