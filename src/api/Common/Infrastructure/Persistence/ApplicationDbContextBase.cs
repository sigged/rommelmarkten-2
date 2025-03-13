using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Interfaces;
using Rommelmarkten.Api.Common.Domain;
using Rommelmarkten.Api.Common.Infrastructure.Identity;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Rommelmarkten.Api.Common.Infrastructure.Persistence
{
    //public abstract class ApplicationDbContextBase : IdentityDbContext<ApplicationUser>
    //{
    //    protected readonly ICurrentUserService _currentUserService;
    //    protected readonly IDateTime _dateTime;
    //    protected readonly IDomainEventService _domainEventService;

    //    [RequiresUnreferencedCode(
    //    "EF Core isn't fully compatible with trimming, and running the application may generate unexpected runtime failures. "
    //    + "Some specific coding pattern are usually required to make trimming work properly, see https://aka.ms/efcore-docs-trimming for "
    //    + "more details.")]
    //    [RequiresDynamicCode(
    //    "EF Core isn't fully compatible with NativeAOT, and running the application may generate unexpected runtime failures.")]
    //    public ApplicationDbContextBase(
    //        DbContextOptions options,
    //        ICurrentUserService currentUserService,
    //        IDomainEventService domainEventService,
    //        IDateTime dateTime)
    //    {
    //        _currentUserService = currentUserService;
    //        _domainEventService = domainEventService;
    //        _dateTime = dateTime;
    //    }

    //    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    //    {
    //        foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<IAuditable> entry in ChangeTracker.Entries<IAuditable>())
    //        {
    //            switch (entry.State)
    //            {
    //                case EntityState.Added:
    //                    entry.Entity.CreatedBy = _currentUserService.UserId ?? string.Empty;
    //                    entry.Entity.Created = _dateTime.Now;
    //                    break;

    //                case EntityState.Modified:
    //                    entry.Entity.LastModifiedBy = _currentUserService.UserId ?? string.Empty;
    //                    entry.Entity.LastModified = _dateTime.Now;
    //                    break;
    //            }
    //        }

    //        var result = await base.SaveChangesAsync(cancellationToken);

    //        await DispatchEvents();

    //        return result;
    //    }

    //    public async Task<int> SaveChangesWithoutAutoAuditables(CancellationToken cancellationToken = new CancellationToken())
    //    {
    //        var result = await base.SaveChangesAsync(cancellationToken);

    //        await DispatchEvents();

    //        return result;
    //    }

    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    //        base.OnModelCreating(builder);
    //    }

    //    protected async Task DispatchEvents()
    //    {
    //        while (true)
    //        {
    //            var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
    //                .Select(x => x.Entity.DomainEvents)
    //                .SelectMany(x => x)
    //                .Where(domainEvent => !domainEvent.IsPublished)
    //                .FirstOrDefault();
    //            if (domainEventEntity == null) break;

    //            domainEventEntity.IsPublished = true;
    //            await _domainEventService.Publish(domainEventEntity);
    //        }
    //    }
    //}
}
