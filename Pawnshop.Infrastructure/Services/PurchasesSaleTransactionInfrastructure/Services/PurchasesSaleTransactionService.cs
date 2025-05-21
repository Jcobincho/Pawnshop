using Pawnshop.Application.ClientsApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Commands.AddPurchaseSaleTransactionDocument;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
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
    }
}
