namespace Pawnshop.Application.Common.Pagination
{
    public class PagedResult<TData>
    {
        public List<TData> Data { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public PagedResult()
        {
            Data = new List<TData>();
        }

        public PagedResult(List<TData> data, int totalCount, int pageNumber, int pageSize)
        {
            Data = data ?? new List<TData>();
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            HasPreviousPage = pageNumber > 1;
            HasNextPage = pageNumber < TotalPages;
        }
    }
}
