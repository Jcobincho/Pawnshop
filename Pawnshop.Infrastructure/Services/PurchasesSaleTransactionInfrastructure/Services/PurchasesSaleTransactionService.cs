using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.Common.Pagination;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.DeletePurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.UpdatePurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Dto;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Dto.DtoExtension;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetEverySalesTransaction;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetPurchasesForClient;
using Pawnshop.Domain.Entities.Transactions;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Services
{
    internal sealed class PurchasesSaleTransactionService : IPurchasesSaleTransactionCommandService, IPurchasesSaleTransactionQueryService
    {
        private readonly DbContext _dbContext;
        private readonly IClientsQueryService _clientsQueryService;

        public PurchasesSaleTransactionService(DbContext dbContext, IClientsQueryService clientsQueryService)
        {
            _dbContext = dbContext;
            _clientsQueryService = clientsQueryService;
        }

        public async Task<Guid> AddPurchaseSaleTransactionAsync(AddPurchaseSaleTransactionDocumentCommand command, CancellationToken cancellationToken)
        {
            var newPurchaseSaleTransaction = new PurchaseSaleTransaction
            {
                TypeOfTransaction = command.TypeOfTransaction,
                TransactionDate = command.TransactionDate,
                Description = command.Description
            };
            
            if(command.TypeOfTransaction == Domain.Enums.TypeOfTransactionEnum.Purchase)
            {
                var isClientExist = await _clientsQueryService.IsClientExistAsync((Guid)command.ClientId, cancellationToken);

                if(!isClientExist)
                    throw new NotFoundException("Client doesn't exist.");

                newPurchaseSaleTransaction.ClientId = command.ClientId;
            }
            else
            {
                newPurchaseSaleTransaction.ClientId = Guid.Empty;
            }

            await _dbContext.PurchasesSaleTransaction.AddAsync(newPurchaseSaleTransaction, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newPurchaseSaleTransaction.Id;
        }

        public async Task UpdatePurchaseSaleTransactionAsync(UpdatePurchaseSaleTransactionDocumentCommand command, CancellationToken cancellationToken)
        {
            var document = await GetPurchaseSaleTransactionByIdAsync(command.PurchaseSaleTransactionDocumentId, cancellationToken);

            document.TypeOfTransaction = command.TypeOfTransaction;
            document.TransactionDate = command.TransactionDate;
            document.Description = command.Description;

            if (command.TypeOfTransaction == Domain.Enums.TypeOfTransactionEnum.Purchase)
            {
                var isClientExist = await _clientsQueryService.IsClientExistAsync((Guid)command.ClientId, cancellationToken);

                if (!isClientExist)
                    throw new NotFoundException("Client doesn't exist.");

                document.ClientId = command.ClientId;
            }
            else
            {
                document.ClientId = Guid.Empty;
            }

            _dbContext.PurchasesSaleTransaction.Update(document);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeletePurchaseSaleTransactionAsync(DeletePurchaseSaleTransactionDocumentCommand command, CancellationToken cancellationToken)
        {
            var document = await GetPurchaseSaleTransactionByIdAsync(command.PurchaseSaleTransactionDocumentId, cancellationToken);

            _dbContext.PurchasesSaleTransaction.Remove(document);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<PurchaseSaleTransaction> GetPurchaseSaleTransactionByIdAsync(Guid purchaseSateTransactionId, CancellationToken cancellationToken)
        {
            var purchaseSaleTransaction = await _dbContext.PurchasesSaleTransaction.FindAsync(purchaseSateTransactionId, cancellationToken);

            if(purchaseSaleTransaction == null)
            {
                throw new NotFoundException("Document doesn't exist.");
            }

            return purchaseSaleTransaction;
        }

        public async Task<PagedResult<SalesTransactionDto>> GetEverySalesTransactionsPagedAsDtoAsync(GetEverySalesTransactionQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _dbContext.PurchasesSaleTransaction
                                             .Where(x => x.TypeOfTransaction == Domain.Enums.TypeOfTransactionEnum.Sale)
                                             .OrderByDescending(x => x.TransactionDate);

            var totalCount = await dbQuery.CountAsync(cancellationToken);

            var salesTransactions = await dbQuery.Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
                                                 .Take(query.PaginationParameters.PageSize)
                                                 .Select(x => x.SalesTransactionParaseToDto())
                                                 .ToListAsync(cancellationToken);

            return new PagedResult<SalesTransactionDto>
            (
                salesTransactions,
                totalCount,
                query.PaginationParameters.PageNumber,
                query.PaginationParameters.PageSize
            );
        }

        public async Task<PagedResult<PurchasesTransactionDto>> GetPurchasesForClientPagedAsDtoAsync(GetPurchasesForClientQuery query, CancellationToken cancellationToken)
        {
            var isClientExits = await _clientsQueryService.IsClientExistAsync(query.ClientId, cancellationToken);

            if (!isClientExits)
                throw new NotFoundException("Client doesn't exist.");

            var dbQuery = _dbContext.PurchasesSaleTransaction.Where(x => x.ClientId == query.ClientId)
                                                             .OrderByDescending(x => x.TransactionDate);

            var totalCount = await dbQuery.CountAsync(cancellationToken);

            var purchasesTransactions = await dbQuery.Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
                                                 .Take(query.PaginationParameters.PageSize)
                                                 .Select(x => x.PurchasesTransactionParseToDto())
                                                 .ToListAsync(cancellationToken);

            return new PagedResult<PurchasesTransactionDto>
            (
                purchasesTransactions,
                totalCount,
                query.PaginationParameters.PageNumber,
                query.PaginationParameters.PageSize
            );
        }
    }
}
