namespace Rommelmarkten.Api.Domain.Common
{
    public abstract class EntityBase
    {
        public virtual Guid Id { get; set; }
    }

    public abstract class EntityBase<T>
        where T : notnull
    {
        public virtual required T Id { get; set; }
    }
}
