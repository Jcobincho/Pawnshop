using MediatR;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Domain.AuthTokens;

namespace Pawnshop.Application.UsersApplication.Commands.LoginUser
{
    public sealed class LoginUserHandler : IRequestHandler<LoginUserCommand, JsonWebToken>
    {
        private readonly IUsersCommandService _usersCommandService;

        public LoginUserHandler(IUsersCommandService usersCommandService)
        {
            _usersCommandService = usersCommandService;
        }

        public async Task<JsonWebToken> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = await _usersCommandService.LoginUserAsync(request, cancellationToken);

            return jwtToken;
        }
    }
}
