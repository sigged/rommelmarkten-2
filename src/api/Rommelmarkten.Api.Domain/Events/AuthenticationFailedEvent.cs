using Rommelmarkten.Api.Domain.Common;

namespace Rommelmarkten.Api.Domain.Events
{
    public class AuthenticationFailedEvent<TResult> : DomainEvent
    {
        public AuthenticationFailedEvent(string userName, TResult result)
        {
            UserName = userName;
            Result = result;
        }

        public string UserName { get; }
        public TResult Result { get; }
    }
}
