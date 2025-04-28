using Pawnshop.Application.WorkplacesApplication.Commands.AddWorkplace;
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

    }
}
