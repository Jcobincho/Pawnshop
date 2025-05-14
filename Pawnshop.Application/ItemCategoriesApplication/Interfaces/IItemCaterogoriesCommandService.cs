using Pawnshop.Application.ItemCategoriesApplication.Commands.AddCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.DeleteCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.UpdateCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Interfaces
{
    public interface IItemCaterogoriesCommandService
    {
        Task<Guid> AddCategoryServiceAsync(AddCategoryCommand command, CancellationToken cancellationToken);
        Task UpdateCategoryServiceAsync(UpdateCategoryCommand command, CancellationToken cancellationToken);
        Task DeleteCategoryServiceAsync(DeleteCategoryCommand command, CancellationToken cancellationToken);
    }
}
