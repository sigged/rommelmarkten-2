using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Rommelmarkten.Api.Common.Application.Pagination;

namespace Rommelmarkten.Api.Common.Application.Mappings
{
    public static class MappingExtensions
    {
        [Obsolete]
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        [Obsolete]
        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}
