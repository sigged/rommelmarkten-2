namespace Rommelmarkten.Api.Common.Domain.Events
{
    public class AuthenticationFailedEvent<TResult> : DomainEvent
    {
        public AuthenticationFailedEvent(string email, TResult result)
        {
            Email = email;
            Result = result;
        }

        public string Email { get; }
        public TResult Result { get; }
    }
}
