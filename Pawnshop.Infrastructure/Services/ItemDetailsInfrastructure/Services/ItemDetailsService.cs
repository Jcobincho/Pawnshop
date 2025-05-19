using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ItemCategoriesApplication.Interfaces;
using Pawnshop.Application.ItemDetailsApplication.Commands.AddItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Commands.DeleteItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Commands.UpdateItemDetail;
using Pawnshop.Application.ItemDetailsApplication.Dto;
using Pawnshop.Application.ItemDetailsApplication.Dto.DtoExtension;
using Pawnshop.Application.ItemDetailsApplication.Interfaces;
using Pawnshop.Domain.Entities.Item;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.ItemDetailsInfrastructure.Services
{
    internal sealed class ItemDetailsService : IItemDetailsCommandService, IItemDetailsQueryService
    {
        private readonly DbContext _dbContext;
        private readonly IItemCategoriesQueryService _itemCategoriesQueryService;

        public ItemDetailsService(DbContext dbContext, IItemCategoriesQueryService itemCategoriesQueryService)
        {
            _dbContext = dbContext;
            _itemCategoriesQueryService = itemCategoriesQueryService;
        }

        public async Task<Guid> AddItemDetailAsync(AddItemDetailsCommand command, CancellationToken cancellationToken)
        {
            bool isCategoryExist = await _itemCategoriesQueryService.CategoryExistsAsync(command.ItemCategoryId, cancellationToken);

            if (!isCategoryExist)
            {
                throw new NotFoundException("Category doesn't exist.");
            }

            var newItemDetail = new ItemDetail()
            {
                Name = command.Name,
                ItemCategoryId = command.ItemCategoryId,
                Brand = command.Brand,
                Model = command.Model,
                SerialNumber = command.SerialNumber,
                AddedOn = command.AddedOn,
                Comments = command.Comments,
            };

            await _dbContext.ItemsDetail.AddAsync(newItemDetail, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newItemDetail.Id;
        }

        public async Task UpdateItemDetailAsync(UpdateItemDetailCommand command, CancellationToken cancellationToken)
        {
            bool isCategoryExist = await _itemCategoriesQueryService.CategoryExistsAsync(command.ItemCategoryId, cancellationToken);

            if (!isCategoryExist)
            {
                throw new NotFoundException("Category doesn't exist.");
            }

            var item = await GetItemDetailByIdAsync(command.UpdateItemId, cancellationToken);

            item.Name = command.Name;
            item.ItemCategoryId = command.ItemCategoryId;
            item.Brand = command.Brand;
            item.Model = command.Model;
            item.SerialNumber = command.SerialNumber;
            item.AddedOn = command.AddedOn;
            item.Comments = command.Comments;

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

        public async Task<List<ItemDetailDto>> GetAllItemDetailsAsDtoAsync(CancellationToken cancellationToken)
        {
            var itemDetailsList = await _dbContext.ItemsDetail.Select(x => x.ItemDetailParseToDto()).ToListAsync();

            return itemDetailsList;
        }

        public async Task<bool> ItemDetailExistsAsync(Guid itemDetailId, CancellationToken cancellationToken)
        {
            return await _dbContext.ItemsDetail.AnyAsync(x => x.Id == itemDetailId, cancellationToken);
        }
    }
}
