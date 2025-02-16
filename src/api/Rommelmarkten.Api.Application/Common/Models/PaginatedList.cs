using Microsoft.EntityFrameworkCore;

namespace Rommelmarkten.Api.Application.Common.Models
{
    public class PaginatedList<T>
    {
        public IQueryable<T> Items { get; }
        public int PageIndex { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public PaginatedList(IQueryable<T> items, int pageIndex, int pageSize, int count)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
