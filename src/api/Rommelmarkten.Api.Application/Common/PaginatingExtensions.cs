using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Application.Common.Models;

namespace Rommelmarkten.Api.Application.Common
{
    public static class PaginatingExtensions
    {
        public static async Task<PaginatedList<TEntity>> ToPagesAsync<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize)
        {
            var count = await query.CountAsync();
            var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new PaginatedList<TEntity>(items, pageIndex, pageSize, count);
        }

        public static async Task<PaginatedList<TProject>> ToPagesAsync<TEntity, TProject>(this IQueryable<TEntity> query, int pageIndex, int pageSize, IConfigurationProvider mapperConfiguration)
        {
            var count = await query.CountAsync();
            var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ProjectTo<TProject>(mapperConfiguration);
            return new PaginatedList<TProject>(items, pageIndex, pageSize, count);
        }
    }
}
