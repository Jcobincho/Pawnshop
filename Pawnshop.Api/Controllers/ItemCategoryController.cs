using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pawnshop.Application.ItemCategoriesApplication.Commands.AddItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.DeleteItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Commands.UpdateItemCategory;
using Pawnshop.Application.ItemCategoriesApplication.Queries.GetAllItemCategories;

namespace Pawnshop.Api.Controllers
{
    [Route("controller")]
    [Authorize]
    public class ItemCategoriesController : BaseController<AddItemCategoryCommand, UpdateItemCategoryCommand, DeleteItemCategoryCommand, GetAllItemCategoriesQuery>
    {
    }
}
