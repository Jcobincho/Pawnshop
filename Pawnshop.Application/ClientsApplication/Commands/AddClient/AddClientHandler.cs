using MediatR;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.ClientsApplication.Responses;

namespace Pawnshop.Application.ClientsApplication.Commands.AddClient
{
    public sealed class AddClientHandler : IRequestHandler<AddClientCommand, AddClientResponse>
    {
        private readonly IClientsCommandService _clientsCommandService;

        public AddClientHandler(IClientsCommandService clientsCommandService)
        {
            _clientsCommandService = clientsCommandService;
        }

        public async Task<AddClientResponse> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
