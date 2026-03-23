using Pawnshop.Application.UsersApplication.Dto;

namespace Pawnshop.Application.UsersApplication.Responses
{
    public sealed class GetAllUsersResponse
    {
        public List<GetAllUsersDto> Users { get; set; }
    }
}
