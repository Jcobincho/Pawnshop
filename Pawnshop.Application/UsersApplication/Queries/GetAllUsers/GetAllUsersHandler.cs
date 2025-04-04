using MediatR;
using Pawnshop.Application.UsersApplication.Interfaces;
using Pawnshop.Application.UsersApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.UsersApplication.Queries.GetAllUsers
{
    public sealed class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersResponse>
    {
        private readonly IUsersQueryService _usersQueryService;

        public GetAllUsersHandler(IUsersQueryService usersQueryService)
        {
            _usersQueryService = usersQueryService;
        }

        public async Task<GetAllUsersResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _usersQueryService.GetAllUsersAsync(cancellationToken);

            return new GetAllUsersResponse
            {
                Users = users,
            };
        }
    }
}
