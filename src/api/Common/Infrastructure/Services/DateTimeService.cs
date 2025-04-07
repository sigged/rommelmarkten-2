using Rommelmarkten.Api.Common.Application.Interfaces;

namespace Rommelmarkten.Api.Common.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
