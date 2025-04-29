using Pawnshop.Application.WorkplacesApplication.Commands.AddWorkplace;
using Pawnshop.Application.WorkplacesApplication.Commands.DeleteWorkplace;
using Pawnshop.Application.WorkplacesApplication.Commands.UpdateWorkplace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.WorkplacesApplication.Interfaces
{
    public interface IWorkplacesCommandService
    {
        Task<Guid> AddWorkplaceAsync(AddWorkplaceCommand command, CancellationToken cancellationToken);
        Task UpdateWorkplaceAsync(UpdateWorkplaceCommand command, CancellationToken cancellationToken);
        Task DeleteWorkplaceAsync(DeleteWorkplaceCommand command, CancellationToken cancellationToken);
    }
}
