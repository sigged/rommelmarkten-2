using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Rommelmarkten.Api.Common.Application.Pagination
{
    public static class PaginatingExtensions
    {
        public static async Task<PaginatedList<TEntity>> ToPagesAsync<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize)
        {
            int count = await Task.FromResult(query.Count()); //todo: can we get back to the line below for non EF Core queryables?
            //int count = await query.CountAsync(); //wont work on normal IQueryable like GetRoles
            var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new PaginatedList<TEntity>(items, pageIndex, pageSize, count);
        }

        public static async Task<PaginatedList<TProject>> ToPagesAsync<TEntity, TProject>(this IQueryable<TEntity> query, int pageIndex, int pageSize, IConfigurationProvider mapperConfiguration)
        {
            int count = await Task.FromResult(query.Count()); //todo: can we get back to the line below for non EF Core queryables?
            //int count = await query.CountAsync(); //wont work on normal IQueryable like GetRoles
            var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ProjectTo<TProject>(mapperConfiguration);
            return new PaginatedList<TProject>(items, pageIndex, pageSize, count);
        }

    }
}
