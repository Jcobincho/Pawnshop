using MediatR;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Application.EmployeesApplication.Responses;

namespace Pawnshop.Application.EmployeesApplication.Commands.EditEmployee
{
    public sealed class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeResponse>
    {
        private readonly IEmployeesCommandService _employeesCommandService;

        public UpdateEmployeeHandler(IEmployeesCommandService employeesCommandService)
        {
            _employeesCommandService = employeesCommandService;
        }

        public async Task<UpdateEmployeeResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeesCommandService.EditEmployeeAsync(request, cancellationToken);

            return new UpdateEmployeeResponse();
        }
    }
}
