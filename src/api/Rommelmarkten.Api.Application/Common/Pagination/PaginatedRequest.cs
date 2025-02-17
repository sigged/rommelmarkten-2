namespace Rommelmarkten.Api.Application.Common.Pagination
{
    public class PaginatedRequest
    {
        public PaginatedRequest() : this(1, 50)
        {

        }

        public PaginatedRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }


}
