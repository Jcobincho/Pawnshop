using MediatR;
using Pawnshop.Application.UsersApplication.Commands.CreateUser;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Responses;

namespace Pawnshop.Application.UsersApplicaiton.Commands.CreateUser
{
    public sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUsersCommandService _usersCommandService;

        public CreateUserHandler(IUsersCommandService usersCommandService)
        {
            _usersCommandService = usersCommandService;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Guid createdUserId = await _usersCommandService.CreateUserAsync(request, cancellationToken);

            return new CreateUserResponse()
            {
                Message = "User created successfully.",
                UserId = createdUserId
            };
        }
    }
}
