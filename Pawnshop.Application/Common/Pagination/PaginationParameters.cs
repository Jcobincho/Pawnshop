namespace Pawnshop.Application.Common.Pagination
{
    public class PaginationParameters
    {
        private const int MaxPageSize = 100;
        private int pageSize = 30;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
