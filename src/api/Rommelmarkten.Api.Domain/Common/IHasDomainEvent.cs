using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Rommelmarkten.Api.Domain.Common
{
    public interface IHasDomainEvent
    {
        public ICollection<DomainEvent> DomainEvents { get; set; }
    }
}
