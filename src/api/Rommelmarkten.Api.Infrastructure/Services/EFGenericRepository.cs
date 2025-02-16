using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rommelmarkten.Api.Infrastructure.Services
{

    ///// <summary>
    ///// Base repository class which implements much of the standard repository logic
    ///// This class is Entity Framework specific (uses DbSet & DbContext)
    ///// </summary>
    ///// <typeparam name="TEntity"></typeparam>
    //public class EFGenericRepository<TEntity> : IGenericRepository<TEntity>
    //    where TEntity : class
    //{
    //    internal DbContext context;
    //    protected readonly DbSet<TEntity> dbSet;

    //    public EFGenericRepository(DbContext context)
    //    {
    //        this.context = context;
    //        dbSet = context.Set<TEntity>();
    //    }

    //    public virtual IQueryable<TEntity> SelectAsQuery(
    //       Expression<Func<TEntity, bool>> filter = null,
    //       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    //       string includeProperties = "")
    //    {
    //        //IQueryable<TEntity> query = this.dbSet.AsExpandable();
    //        ExpandableQuery<TEntity> query = (ExpandableQuery<TEntity>)this.dbSet.AsExpandable();


    //        if (filter != null)
    //            query = (ExpandableQuery<TEntity>)query.Where(filter); //query = query.Where(filter);

    //        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
    //            query = (ExpandableQuery<TEntity>)query.Include<TEntity>(includeProperty); //query = query.Include<TEntity>(includeProperty);

    //        if (orderBy != null)
    //        {
    //            return orderBy(query);
    //        }
    //        else
    //        {
    //            return query;
    //        }
    //    }

    //    public virtual ICollection<TEntity> Select(
    //        Expression<Func<TEntity, bool>> filter = null,
    //        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    //        string includeProperties = "")
    //    {
    //        return SelectAsQuery(filter, orderBy, includeProperties).ToList();
    //    }

    //    public virtual async Task<PaginatedList<TEntity>> SelectPagedAsync(int page, int pageSize, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties = "")
    //    {
    //        IQueryable<TEntity> query = SelectAsQuery(filter, orderBy, includeProperties);
    //        var skip = (page - 1) * pageSize;

    //        var result = await PaginatedList<TEntity>.CreateAsync(query, page, pageSize);
    //        return result;
    //    }

    //    public virtual TEntity GetById(object id)
    //    {
    //        return dbSet.Find(id);
    //    }

    //    public virtual void Insert(TEntity entity)
    //    {
    //        dbSet.Add(entity);
    //    }

    //    public virtual void Delete(object id)
    //    {
    //        TEntity entityToDelete = dbSet.Find(id);
    //        Delete(entityToDelete);
    //    }

    //    public virtual void Delete(TEntity entityToDelete)
    //    {
    //        if (context.Entry(entityToDelete).State == EntityState.Detached)
    //        {
    //            dbSet.Attach(entityToDelete);
    //        }
    //        dbSet.Remove(entityToDelete);
    //    }

    //    public virtual void Update(TEntity entityToUpdate)
    //    {
    //        dbSet.Attach(entityToUpdate);
    //        context.Entry(entityToUpdate).State = EntityState.Modified;
    //    }

    //}
}
