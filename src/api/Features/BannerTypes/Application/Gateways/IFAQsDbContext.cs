using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Application.Gateways
{
    public interface IFAQsDbContext : IApplicationDbContext
    {

        DbSet<FAQCategory> FAQCategories { get; set; }

        DbSet<FAQItem> FAQItems { get; set; }

    }
}
