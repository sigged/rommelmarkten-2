namespace Rommelmarkten.Api.Features.ShoppingLists.Infrastructure.Realtime.Models
{
    public abstract record EventBaseDto
    {
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
    }


}
