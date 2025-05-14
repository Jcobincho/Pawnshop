using Pawnshop.Application.Base;
using Pawnshop.Application.ItemDetailsApplication.Responses;
using Pawnshop.Domain.Entities.Item;
using System.ComponentModel.DataAnnotations;

namespace Pawnshop.Application.ItemDetailsApplication.Commands.AddItemDetail
{
    public sealed class AddItemDetailsCommand : BaseCommand<AddItemDetailsResponse>
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public Guid ItemCategoryId { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Added date is required.")]
        public DateTime AddedOn { get; set; }
        public string Comments { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } 
    }
}
