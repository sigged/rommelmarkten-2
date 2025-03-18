namespace Rommelmarkten.Api.Common.Domain
{
    public interface IOwnable
    {
        string OwnedBy { get; set; }
    }
}