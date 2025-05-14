namespace Pawnshop.Application.ItemCategoriesApplication.Responses
{
    public sealed class AddItemCategoryResponse
    {
        public Guid CategoryId { get; set; }
        public string Message { get; set; } = "Success.";
    }
}
