using Pawnshop.Application.UsersApplication.Commands.CreateUser;
using Pawnshop.Application.UsersApplication.Commands.LoginUser;
using Pawnshop.Domain.AuthTokens;

namespace Pawnshop.Application.UsersApplication.Interfaces
{
    public interface IUsersCommandService
    {
        public Task<Guid> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken);
        public Task<JsonWebToken> LoginUserAsync(LoginUserCommand command, CancellationToken cancellationToken);
    }
}
