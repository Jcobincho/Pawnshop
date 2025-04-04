using MediatR;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Responses;

namespace Pawnshop.Application.UsersApplication.Commands.EditUser
{
    public sealed class EditUserHandler : IRequestHandler<EditUserCommand, EditUserResponse>
    {
        private readonly IUsersCommandService _usersCommandService;

        public EditUserHandler(IUsersCommandService usersCommandService)
        {
            _usersCommandService = usersCommandService;
        }

        public async Task<EditUserResponse> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            await _usersCommandService.EditUserAsync(request, cancellationToken);

            return new EditUserResponse();
        }
    }
}
