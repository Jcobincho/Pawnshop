using Pawnshop.Application.ItemCategoriesApplication.Commands.AddItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.DeleteItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.UpdateItemCategory;

namespace Pawnshop.Application.ItemCategoriesApplication.Interfaces
{
    public interface IItemCaterogoriesCommandService
    {
        Task<Guid> AddItemCategoryServiceAsync(AddItemCategoryCommand command, CancellationToken cancellationToken);
        Task UpdateItemCategoryServiceAsync(UpdateItemCategoryCommand command, CancellationToken cancellationToken);
        Task DeleteItemCategoryServiceAsync(DeleteItemCategoryCommand command, CancellationToken cancellationToken);
    }
}
