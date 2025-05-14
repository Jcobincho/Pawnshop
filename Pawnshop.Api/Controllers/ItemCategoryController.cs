using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.ItemCategoriesApplication.Commands.AddCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.DeleteCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.UpdateCategory;
using Pawnshop.Application.ItemCategoriesApplication.Queries.GetAllItemCategories;

namespace Pawnshop.Api.Controllers
{
    [Route("controller")]
    [Authorize]
    public class ItemCategoryController :BaseController<AddCategoryCommand,UpdateCategoryCommand,DeleteCategoryCommand,GetAllItemCategoriesQuery>
    {
    }
}
