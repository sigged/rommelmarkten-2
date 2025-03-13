using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Exceptions;
using Rommelmarkten.Api.Common.Application.Interfaces;
using System.Linq.Expressions;

namespace Rommelmarkten.Api.Common.Infrastructure.Services
{

    /// <summary>
    /// Base repository class which implements much of the standard repository logic
    /// This class is Entity Framework specific (uses DbSet & DbContext)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFRepository<TEntity, TContext> : IEntityRepository<TEntity> 
        where TEntity : class
        where TContext : IApplicationDbContext
    {
        protected readonly TContext context;
        protected readonly DbSet<TEntity> dbSet;

        public EFRepository(TContext context)
        {
            if (context is DbContext)
            {
                this.context = context;
                dbSet = this.context.Set<TEntity>();
            }
            else
            {
                throw new ArgumentException($"Parameter {nameof(context)} must inherit from {nameof(DbContext)} for {nameof(EFRepository<TEntity, TContext>)} to work.");
            }
        }

        public virtual IQueryable<TEntity> SelectAsQuery(
            Expression<Func<TEntity, object>>[]? includes = null,
            Expression<Func<TEntity, bool>>[]? filters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            bool noTracking = true
        )
        {
            IQueryable<TEntity> query;
            if (noTracking)
            {
                query = dbSet.AsNoTracking();
            }
            else
            {
                query = dbSet.AsQueryable();
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>[]? filters = null, CancellationToken cancellationToken = default)
        {
            var query = SelectAsQuery(filters: filters);
            return await query.AnyAsync(cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>>[]? filters = null, CancellationToken cancellationToken = default)
        {
            var query = SelectAsQuery(filters: filters);
            return await query.CountAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            var entity = await dbSet.FindAsync([id], cancellationToken);
            return entity ??
                throw new NotFoundException($"{typeof(TEntity).Name} identified as {id} was not found");
        }

        public virtual async Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            TEntity entityToDelete = await GetByIdAsync(id, cancellationToken);
            Delete(entityToDelete);
            await context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Insert(entity);
            await context.SaveChangesAsync(cancellationToken);

            context.ChangeTracker.Clear();
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Update(entity);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
