using Pawnshop.Application.ClientsApplication.Responses;
using Pawnshop.Application.Common.Base;
using Pawnshop.Application.Common.Pagination;

namespace Pawnshop.Application.ClientsApplication.Queries.GetAllClients
{
    public sealed class GetAllClientsQuery : BaseQuery<GetAllClientsResponse>
    {
        public PaginationParameters PaginationParameters { get; set; }
    }
}
