namespace Rommelmarkten.Api.Common.Domain
{
    public abstract class EntityBase
    {
        public virtual Guid Id { get; set; }
    }

    public abstract class EntityBase<T>
        where T : notnull
    {
        public virtual T Id { get; set; } = default!;
    }
}
