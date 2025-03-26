using System.Collections;

namespace Rommelmarkten.ApiClient.Common.Payloads
{
    public class PaginatedList<T>() : IApiPayload
    {
        public ICollection<T> Items { get; set; } = [];

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

    }

}
