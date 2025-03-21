using MediatR;
using Pawnshop.Application.Users.Interfaces;
using Pawnshop.Application.Users.Responses;
using Pawnshop.Application.UsersApplication.Commands.CreateUser;

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
