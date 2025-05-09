﻿using Rommelmarkten.Api.Domain.Common;
using Rommelmarkten.Api.Domain.Users;

namespace Rommelmarkten.Api.Domain.Events
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
