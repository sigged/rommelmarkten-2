using Rommelmarkten.Api.Common.Application.Pagination;

namespace Rommelmarkten.Api.Common.Application.Models
{
    public class PaginatedResult<TItem> : ResultBase<Result, string>
    {
        public PaginatedResult(PaginatedList<TItem> collection) : base()
        {
            Collection = collection;
        }

        public PaginatedResult(bool succeeded, PaginatedList<TItem> collection, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Collection = collection;
            Errors = errors.ToArray();
        }

        public PaginatedList<TItem> Collection { get; set; } = new PaginatedList<TItem>(new TItem[0].AsQueryable(), 0,0,0);
    }
}
