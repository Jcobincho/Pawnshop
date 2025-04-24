using Pawnshop.Application.ClientsApplication.Dto;

namespace Pawnshop.Application.ClientsApplication.Responses
{
    public sealed class GetAllClientsResponse
    {
        public List<ClientDto> AllClientsList { get; set; }
    }
}
