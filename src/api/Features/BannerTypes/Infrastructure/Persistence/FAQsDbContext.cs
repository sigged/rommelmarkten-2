using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.FAQs.Application.Gateways;
using Rommelmarkten.Api.Features.FAQs.Domain;

namespace Rommelmarkten.Api.Features.FAQs.Infrastructure.Persistence
{
    public class FAQsDbContext : ApplicationDbContext, IFAQsDbContext
    {
        public FAQsDbContext(DbContextOptions options, ICurrentUserService currentUserService, IDomainEventService domainEventService, IDateTime dateTime)
            : base(options, currentUserService, domainEventService, dateTime)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(FAQsDbContext).Assembly);
        }

        public required DbSet<FAQCategory> FAQCategories { get; set; }

        public required DbSet<FAQItem> FAQItems { get; set; }

    }
}
