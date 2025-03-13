using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Infrastructure.Persistence;
using Rommelmarkten.Api.Features.FAQs.Application.Gateways;
using Rommelmarkten.Api.Features.FAQs.Domain;
using System.Reflection;

namespace Rommelmarkten.Api.Features.FAQs.Infrastructure.Persistence
{
    public class FAQsDbContext : ApplicationDbContextBase, IFAQsDbContext
    {
        public FAQsDbContext(
            DbContextOptions<FAQsDbContext> options,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime
            )
                : base(options, currentUserService, domainEventService, dateTime)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public required DbSet<FAQCategory> FAQCategories { get; set; }

        public required DbSet<FAQItem> FAQItems { get; set; }

    }
}
