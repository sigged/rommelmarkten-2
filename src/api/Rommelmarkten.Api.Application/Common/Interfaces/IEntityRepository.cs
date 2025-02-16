using Rommelmarkten.Api.Application.Common.Models;
using System.Linq.Expressions;

namespace Rommelmarkten.Api.Application.Common.Interfaces
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>[]? filters = null, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>[]? filters = null, CancellationToken cancellationToken = default);
        Task DeleteByIdAsync(object id, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        IQueryable<TEntity> SelectAsQuery(Expression<Func<TEntity, object>>[]? includes = null, Expression<Func<TEntity, bool>>[]? filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool noTracking = true);
        Task<PaginatedList<TEntity>> SelectPagedAsync(int page, int pageSize, Expression<Func<TEntity, object>>[]? includes = null, Expression<Func<TEntity, bool>>[]? filters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool noTracking = true, CancellationToken cancellationToken = default);
        void Update(TEntity entityToUpdate);
        void Delete(TEntity entityToDelete);
        void Insert(TEntity entityToDelete);
    }
}