using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Application.ItemCategoriesApplication.Responses
{
    public sealed class AddCategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string MyProperty { get; set; } = "Success";
    }
}
