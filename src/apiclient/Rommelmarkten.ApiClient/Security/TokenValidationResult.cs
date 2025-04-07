namespace Rommelmarkten.ApiClient.Security
{
    public enum TokenValidationResult
    {
        Valid = 0,
        InvalidIssuer = 1,
        InvalidAudience = 2,
        Expired = 3,
        NotYetValid = 4,
        Malformed = 5,
    }
}
