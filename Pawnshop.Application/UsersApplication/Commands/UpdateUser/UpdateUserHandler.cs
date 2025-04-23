using MediatR;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Responses;

namespace Pawnshop.Application.UsersApplication.Commands.EditUser
{
    public sealed class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IUsersCommandService _usersCommandService;

        public UpdateUserHandler(IUsersCommandService usersCommandService)
        {
            _usersCommandService = usersCommandService;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await _usersCommandService.UpdateUserAsync(request, cancellationToken);

            return new UpdateUserResponse();
        }
    }
}
