namespace Rommelmarkten.Api.Common.Domain.Events
{
    public class UserAuthenticatedEvent<TResult> : DomainEvent
    {
        public UserAuthenticatedEvent(IUser user, TResult result)
        {
            User = user;
            Result = result;
        }

        public IUser User { get; }
        public TResult Result { get; }
    }
}
