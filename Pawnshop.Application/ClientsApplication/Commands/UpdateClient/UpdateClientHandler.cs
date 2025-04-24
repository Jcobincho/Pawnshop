using MediatR;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.ClientsApplication.Responses;

namespace Pawnshop.Application.ClientsApplication.Commands.UpdateClient
{
    public sealed class UpdateClientHandler : IRequestHandler<UpdateClientCommand, UpdateClientResponse>
    {
        private readonly IClientsCommandService _clientsCommandService;

        public UpdateClientHandler(IClientsCommandService clientsCommandService)
        {
            _clientsCommandService = clientsCommandService;
        }

        public async Task<UpdateClientResponse> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            await _clientsCommandService.UpdateClientAsync(request, cancellationToken);

            return new UpdateClientResponse();
        }
    }
}
