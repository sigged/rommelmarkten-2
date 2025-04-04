﻿namespace Rommelmarkten.ApiClient.Features.Users
{
    public partial class UsersClient
    {
        public class UpdateProfileCommand
        {
            public required string UserId { get; set; }
            public required string FirstName { get; set; }
            public required string LastName { get; set; }
            public required string Email { get; set; }
            public bool HasConsented { get; set; }
        }
    }
}
