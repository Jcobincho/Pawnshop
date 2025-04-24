using MediatR;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.ClientsApplication.Responses;

namespace Pawnshop.Application.ClientsApplication.Queries.GetAllClients
{
    public sealed class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, GetAllClientsResponse>
    {
        private readonly IClientsQueryService _clientsQueryService;

        public GetAllClientsHandler(IClientsQueryService clientsQueryService)
        {
            _clientsQueryService = clientsQueryService;
        }
        public async Task<GetAllClientsResponse> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var clients = await _clientsQueryService.GetAllClientsAsDtoAsync(cancellationToken);

            return new GetAllClientsResponse
            {
                AllClientsList = clients
            };
        }
    }
}
