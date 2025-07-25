namespace FinanceManager.Domain.Dtos.Controllers.Transactions.Requests
{
    public class UpdateTransactionRequest
    {
        public required string TransactionId { get; set; }
        public required int PotId { get; set; }
    }
}
