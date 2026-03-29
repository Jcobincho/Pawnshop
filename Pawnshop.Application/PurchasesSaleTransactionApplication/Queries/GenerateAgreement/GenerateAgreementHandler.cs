using MediatR;
using Pawnshop.Application.PdfGeneratorApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Customers.GenerateAgreement;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Interfaces;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Producers;
using Pawnshop.Application.PurchasesSaleTransactionApplication.Responses;

namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GenerateAgreement
{
    public sealed class GenerateAgreementHandler : IRequestHandler<GenerateAgreementQuery, GenerateAgreementResponse>
    {
        private readonly IPurchaseSaleTransactionEventPublisher _purchaseSaleTransactionEventPublisher;
        private readonly IPurchasesSaleTransactionQueryService _purchasesSaleTransactionQueryService;
        private readonly IPdfGeneratorService _pdfGeneratorService;

        public GenerateAgreementHandler(
            IPurchaseSaleTransactionEventPublisher purchaseSaleTransactionEventPublisher,
            IPurchasesSaleTransactionQueryService purchasesSaleTransactionQueryService,
            IPdfGeneratorService pdfGeneratorService)
        {
            _purchaseSaleTransactionEventPublisher = purchaseSaleTransactionEventPublisher;
            _purchasesSaleTransactionQueryService = purchasesSaleTransactionQueryService;
            _pdfGeneratorService = pdfGeneratorService;
        }

        public async Task<GenerateAgreementResponse> Handle(GenerateAgreementQuery request, CancellationToken cancellationToken)
        {
            var transactionData = await _purchasesSaleTransactionQueryService.GetPurchaseSaleTransactionInfoToAgreementAsync(request.PurchasesSaleTransactionId, cancellationToken);

            if (transactionData == null)
            {
                return new GenerateAgreementResponse();
            }

            var pdfBytes = await _pdfGeneratorService.GeneratePdfAsync(transactionData, "PurchaseAgreementTemplate", cancellationToken);

            try
            {
                var purchaseSaleTransactionEvent = new GenerateAgreementEvent(request.PurchasesSaleTransactionId, request.UserIdFromClaims);
                await _purchaseSaleTransactionEventPublisher.GenerateAgreementPublishAsync(purchaseSaleTransactionEvent, cancellationToken);
            }
            catch (Exception ex)
            {
                // Jeśli RabbitMQ leży, logujemy błąd, ale nie przerywamy zwracania PDF-a użytkownikowi
                Console.WriteLine($"Ostrzeżenie: Nie udało się wysłać zdarzenia do kolejki (RabbitMQ): {ex.Message}");
            }

            return new GenerateAgreementResponse
            {
                PdfBytes = pdfBytes
            };
        }
    }
}
