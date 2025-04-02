using MediatR;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Application.EmployeesApplication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.EmployeesApplication.Queries.GetAllEmployees
{
    public sealed class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, GetAllEmployeesResponse>
    {
        private readonly IEmployeesQueryService _employeesQueryService;

        public GetAllEmployeesHandler(IEmployeesQueryService employeesQueryService)
        {
            _employeesQueryService = employeesQueryService;
        }

        public async Task<GetAllEmployeesResponse> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var allEmployees = await _employeesQueryService.GetAllEmployeesAsDtoAsync(cancellationToken);

            return new GetAllEmployeesResponse()
            {
                AllEmployeesList = await
            
        }
    }
}
