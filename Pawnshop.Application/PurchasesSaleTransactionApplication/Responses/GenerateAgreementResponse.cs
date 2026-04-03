// namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Responses
// {
//     public sealed class GenerateAgreementResponse
//     {
//         public string Message { get; set; } = "Generate in progress.";
//     }
// }


namespace Pawnshop.Application.PurchasesSaleTransactionApplication.Queries.GenerateAgreement
{
    public class GenerateAgreementResponse
    {
        public byte[] PdfBytes { get; set; }
    }
}
