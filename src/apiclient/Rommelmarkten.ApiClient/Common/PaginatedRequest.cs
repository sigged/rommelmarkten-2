namespace Rommelmarkten.ApiClient.Common
{
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
