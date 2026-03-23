using Pawnshop.Application.ClientsApplication.Dto;
using Pawnshop.Application.Common.Pagination;

namespace Pawnshop.Application.ClientsApplication.Responses
{
    public sealed class GetAllClientsResponse
    {
        public PagedResult<ClientDto> AllClientsList { get; set; }
    }
}
