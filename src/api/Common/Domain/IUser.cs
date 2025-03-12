namespace Rommelmarkten.Api.Common.Domain
{
    public interface IUser
    {
        string Id { get; set; }
        string? Email { get; set; }
        string? UserName { get; set; }
        string? FirstName { get; set; }
        string? LastName { get; set; }
    }

}
