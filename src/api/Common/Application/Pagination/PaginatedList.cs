using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Rommelmarkten.Api.Common.Application.Pagination
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

        [JsonIgnore]
        public bool HasPreviousPage => PageIndex > 1;

        [JsonIgnore]
        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
