using Rommelmarkten.Api.Application.Common.Interfaces;
using System;

namespace Rommelmarkten.Api.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
