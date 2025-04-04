using Pawnshop.Application.UsersApplication.Commands.CreateUser;
using Pawnshop.Application.UsersApplication.Commands.EditUser;
using Pawnshop.Application.UsersApplication.Commands.LoginUser;
using Pawnshop.Application.UsersApplication.Commands.Logout;
using Pawnshop.Application.UsersApplication.Commands.RefreshToken;
using Pawnshop.Domain.AuthTokens;

namespace Pawnshop.Application.UsersApplication.Interfaces
{
    public interface IUsersCommandService
    {
        Task<Guid> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken);
        Task<JsonWebToken> LoginUserAsync(LoginUserCommand command, CancellationToken cancellationToken);
        Task<JsonWebToken> RefreshTokenAsync(RefreshTokenCommand command, CancellationToken cancellationToken);
        Task LogoutAsync(LogoutCommand? command, CancellationToken cancellationToken);
        Task EditUserAsync(EditUserCommand command, CancellationToken cancellationToken);
    }
}
