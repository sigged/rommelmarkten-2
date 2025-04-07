namespace Rommelmarkten.Api.Common.Domain
{
    public abstract class AuditableEntity<T> : EntityBase<T>, IAuditable
        where T : notnull
    {
        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
