using Microsoft.EntityFrameworkCore;
using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.ItemInPurchaseSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.DeletePurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.UpdatePurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Application.WorkplacesApplication.Interfaces;
using Pawnshop.Domain.Entities.Transactions;
using Pawnshop.Domain.Enums;
using Pawnshop.Domain.Exceptions;

namespace Pawnshop.Infrastructure.Services.PurchasesSaleTransactionInfrastructure.Services
{
    internal sealed class PurchasesSaleTransactionCommandService : IPurchasesSaleTransactionCommandService
    {
        private readonly DbContext _dbContext;
        private readonly IClientsQueryService _clientsQueryService;
        private readonly IWorkplacesQueryService _workplacesQueryService;
        private readonly IPurchasesSaleTransactionQueryService _purchaseTransactionQueryService;

        public PurchasesSaleTransactionCommandService(IWorkplacesQueryService workplacesQueryService, IClientsQueryService clientsQueryService, IPurchasesSaleTransactionQueryService purchaseTransactionQueryService, DbContext dbContext)
        {
            _workplacesQueryService = workplacesQueryService;
            _clientsQueryService = clientsQueryService;
            _purchaseTransactionQueryService = purchaseTransactionQueryService;
            _dbContext = dbContext;
        }

        public async Task<Guid> AddPurchaseSaleTransactionAsync(AddPurchaseSaleTransactionDocumentCommand command, CancellationToken cancellationToken)
        {
            await IsSymbolUniqueForTypeOfTransaction(command.Symbol, cancellationToken);

            var isWorkplaceExist = await _workplacesQueryService.WorkplaceExistsAsync(command.WorkplaceId, cancellationToken);

            if (!isWorkplaceExist)
                throw new NotFoundException("Workplace doesn't exist.");

            var newPurchaseSaleTransaction = new PurchaseSaleTransaction
            {
                Symbol = command.Symbol,
                TypeOfTransaction = command.TypeOfTransaction,
                TransactionDate = command.TransactionDate,
                Description = command.Description,
                WorkplaceId = command.WorkplaceId,
            };

            if (command.TypeOfTransaction == TypeOfTransactionEnum.Purchase)
            {
                var isClientExist = await _clientsQueryService.IsClientExistAsync((Guid)command.ClientId, cancellationToken);

                if (!isClientExist)
                    throw new NotFoundException("Client doesn't exist.");

                newPurchaseSaleTransaction.ClientId = command.ClientId;
            }

            await _dbContext.PurchasesSaleTransaction.AddAsync(newPurchaseSaleTransaction, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newPurchaseSaleTransaction.Id;
        }

        public async Task UpdatePurchaseSaleTransactionAsync(UpdatePurchaseSaleTransactionDocumentCommand command, CancellationToken cancellationToken)
        {
            await IsSymbolUniqueForTypeOfTransaction(command.Symbol, cancellationToken);

            var document = await _purchaseTransactionQueryService.GetPurchaseSaleTransactionByIdAsync(command.PurchaseSaleTransactionDocumentId, cancellationToken);

            var isWorkplaceExist = await _workplacesQueryService.WorkplaceExistsAsync(command.WorkplaceId, cancellationToken);

            if (!isWorkplaceExist)
                throw new NotFoundException("Workplace doesn't exist.");

            document.Symbol = command.Symbol;
            document.TypeOfTransaction = command.TypeOfTransaction;
            document.TransactionDate = command.TransactionDate;
            document.Description = command.Description;
            document.WorkplaceId = command.WorkplaceId;

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
            var document = await _purchaseTransactionQueryService.GetPurchaseSaleTransactionByIdAsync(command.PurchaseSaleTransactionDocumentId, cancellationToken);

            _dbContext.PurchasesSaleTransaction.Remove(document);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task IsSymbolUniqueForTypeOfTransaction(string symbol, CancellationToken cancellationToken)
        {
            bool isSymbolUniqueForTypeOfTransaction = await _dbContext.PurchasesSaleTransaction.AnyAsync(x => x.Symbol == symbol, cancellationToken);

            if (isSymbolUniqueForTypeOfTransaction)
            {
                throw new BadRequestException("The symbol must be unique within the transaction type area.");
            }
        }
    }
}
