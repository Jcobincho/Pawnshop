using Pawnshop.Application.UsersApplication.Dto;

namespace Pawnshop.Application.UsersApplication.Interfaces
{
    public interface IUsersQueryService
    {
        Task<List<GetAllUsersDto>> GetAllUsersAsync(CancellationToken cancellationToken);
    }
}
