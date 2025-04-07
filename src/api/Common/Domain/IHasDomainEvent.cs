namespace Rommelmarkten.Api.Common.Domain
{
    public interface IHasDomainEvent
    {
        public ICollection<DomainEvent> DomainEvents { get; set; }
    }
}
