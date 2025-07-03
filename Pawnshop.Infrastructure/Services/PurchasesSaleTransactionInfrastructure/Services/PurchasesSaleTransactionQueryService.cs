using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.Common.Pagination;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Dto;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Dto.DtoExtension;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetEverySalesTransaction;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GetPurchasesForClient;
using Pawnshop.Domain.Entities.Transactions;
using Pawnshop.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Services
{
    internal sealed class PurchasesSaleTransactionQueryService : IPurchasesSaleTransactionQueryService
    {
        private readonly DbContext _dbContext;
        private readonly IClientsQueryService _clientsQueryService;

        public PurchasesSaleTransactionQueryService(DbContext dbContext, IClientsQueryService clientsQueryService)
        {
            _dbContext = dbContext;
            _clientsQueryService = clientsQueryService;
        }

        public async Task<PurchaseSaleTransaction> GetPurchaseSaleTransactionByIdAsync(Guid purchaseSateTransactionId, CancellationToken cancellationToken)
        {
            var purchaseSaleTransaction = await _dbContext.PurchasesSaleTransaction.FindAsync(purchaseSateTransactionId, cancellationToken);

            if (purchaseSaleTransaction == null)
            {
                throw new NotFoundException("Document doesn't exist.");
            }

            return purchaseSaleTransaction;
        }

        public async Task<PagedResult<SalesTransactionDto>> GetEverySalesTransactionsPagedAsDtoAsync(GetEverySalesTransactionQuery query, CancellationToken cancellationToken)
        {
            var dbQuery = _dbContext.PurchasesSaleTransaction.Where(x => x.TypeOfTransaction == Domain.Enums.TypeOfTransactionEnum.Sale)
                                                             .Include(x => x.Workplace)
                                                             .OrderByDescending(x => x.TransactionDate);

            var totalCount = await dbQuery.CountAsync(cancellationToken);

            var salesTransactions = await dbQuery.Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
                                                 .Take(query.PaginationParameters.PageSize)
                                                 .Select(x => x.SalesTransactionPraseToDto())
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
                                                             .Include(x => x.Workplace)
                                                             .OrderByDescending(x => x.TransactionDate);

            var totalCount = await dbQuery.CountAsync(cancellationToken);

            var purchasesTransactions = await dbQuery.Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
                                                 .Take(query.PaginationParameters.PageSize)
                                                 .Select(x => x.PurchasesTransactionPraseToDto())
                                                 .ToListAsync(cancellationToken);

            return new PagedResult<PurchasesTransactionDto>
            (
                purchasesTransactions,
                totalCount,
                query.PaginationParameters.PageNumber,
                query.PaginationParameters.PageSize
            );
        }

        public async Task<bool> IsPurchaseSaleTransactionExistAsync(Guid purchaseSaleTransactionId, CancellationToken cancellationToken)
        {
            return await _dbContext.PurchasesSaleTransaction.AnyAsync(x => x.Id == purchaseSaleTransactionId, cancellationToken);
        }
    }
}
