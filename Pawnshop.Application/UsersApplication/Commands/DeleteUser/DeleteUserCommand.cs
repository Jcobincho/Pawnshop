using Pawnshop.Application.Common.Base;
using Pawnshop.Application.UsersApplication.Responses;

namespace Pawnshop.Application.UsersApplication.Commands.DeleteUser
{
    public sealed class DeleteUserCommand : BaseCommand<DeleteUserResponse>
    {
        public Guid UserId { get; set; }
    }
}
