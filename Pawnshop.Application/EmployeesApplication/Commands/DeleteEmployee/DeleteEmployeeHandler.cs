using MediatR;
using Pawnshop.Application.EmployeesApplication.Interfaces;
using Pawnshop.Application.EmployeesApplication.Responses;

namespace Pawnshop.Application.EmployeesApplication.Commands.DeleteEmployee
{
    public sealed class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeResponse>
    {
        private readonly IEmployeesCommandService _employeesCommandService;

        public DeleteEmployeeHandler(IEmployeesCommandService employeesCommandService)
        {
            _employeesCommandService = employeesCommandService;
        }

        public async Task<DeleteEmployeeResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeesCommandService.DeleteEmployeeAsync(request, cancellationToken);

            return new DeleteEmployeeResponse();
        }
    }
}
