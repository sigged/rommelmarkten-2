namespace Rommelmarkten.Api.Application.Common.Models
{
    public struct PaginatedRequest
    {
        public PaginatedRequest() : this(0, 50)
        {
            
        }

        public PaginatedRequest(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }


}
