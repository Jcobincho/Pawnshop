using Pawnshop.Application.UsersApplication.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Interfaces
{
    public interface IUsersQueryService
    {
        Task<List<GetAllUsersDto>> GetAllUsersAsync(CancellationToken cancellationToken);
    }
}
