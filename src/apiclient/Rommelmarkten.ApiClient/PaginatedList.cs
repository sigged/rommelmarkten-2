namespace Rommelmarkten.ApiClient
{
    public record PaginatedList<T>()
    {
        public ICollection<T>? Items { get; }

        public int? PageIndex { get; }

        public int? TotalPages { get; }

        public int? TotalCount { get; }

    }

    public class PaginatedRequest
    {
        public PaginatedRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}
