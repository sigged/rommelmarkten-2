namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        public partial class UserProfileResult
        {
            public string? Id { get; }

            public string? Email { get; }

            public string? UserName { get; }

            public bool? Consented { get; }

            public bool? IsBanned { get; }

            public string? Name { get; }

            public string? Address { get; }

            public string? PostalCode { get; }

            public string? City { get; }

            public string? Country { get; }

            public string? Vat { get; }

            public int? ActivationRemindersSent { get; }

            public DateTimeOffset? LastActivationMailSendDate { get; }

            public DateTimeOffset? ActivationDate { get; }

            public DateTimeOffset? LastActivityDate { get; }

            public DateTimeOffset? LastPasswordResetDate { get; }

            public DateTimeOffset? Created { get; }

            public string? CreatedBy { get; }

            public DateTimeOffset? LastModified { get; }

            public string? LastModifiedBy { get; }

        }
    }
}
