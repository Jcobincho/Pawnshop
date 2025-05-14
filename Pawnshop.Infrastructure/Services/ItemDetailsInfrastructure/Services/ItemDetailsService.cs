using Pawnshop.Application.ItemDetailsApplication.Commands.AddItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Commands.DeleteItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Commands.UpdateItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.ItemDetailsInfrastructure.Services
{
    internal sealed class ItemDetailsService : IItemDetailsCommandService, IItemDetailsQueryService
    {
        private readonly DbContext _dbContext;

        public ItemDetailsService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> AddItemDetailAsync(AddItemDetailsCommand command, CancellationToken cancellationToken)
        {
            var newItemDetail = new ItemDetail()
            {
                Name = command.Name,
                ItemCategoryId = command.ItemCategoryId,
                Brand = command.Brand,
                Model = command.Model,
                SerialNumber = command.SerialNumber,
                AddedOn = command.AddedOn,
                Comments = command.Comments,
                IsAvailable = command.IsAvailable
            };

            await _dbContext.ItemsDetail.AddAsync(newItemDetail, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newItemDetail.Id;
        }

        public async Task UpdateItemDetailAsync(UpdateItemDetailCommand command, CancellationToken cancellationToken)
        {
            var item = await GetItemDetailByIdAsync(command.UpdateItemId, cancellationToken);

            item.Name = command.Name;
            item.ItemCategoryId = command.ItemCategoryId;
            item.Brand = command.Brand;
            item.Model = command.Model;
            item.SerialNumber = command.SerialNumber;
            item.AddedOn = command.AddedOn;
            item.Comments = command.Comments;
            item.IsAvailable = command.IsAvailable;

            _dbContext.ItemsDetail.Update(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteItemDetailAsync(DeleteItemDetailCommand command, CancellationToken cancellationToken)
        {
            var item = await GetItemDetailByIdAsync(command.ItemId, cancellationToken);

            _dbContext.ItemsDetail.Remove(item);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<ItemDetail> GetItemDetailByIdAsync(Guid itemDetailId, CancellationToken cancellationToken)
        {
            var itemDetail = await _dbContext.ItemsDetail.FindAsync(itemDetailId, cancellationToken);

            if(itemDetail == null)
            {
                throw new NotFoundException("Item doesn't exist.");
            }

            return itemDetail;
        }
    }
}
