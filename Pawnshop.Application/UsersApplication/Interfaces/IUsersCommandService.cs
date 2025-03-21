using Pawnshop.Application.UsersApplication.Commands.CreateUser;

namespace Pawnshop.Application.Users.Interfaces
{
    public interface IUsersCommandService
    {
        public Task<Guid> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken);
    }
}
