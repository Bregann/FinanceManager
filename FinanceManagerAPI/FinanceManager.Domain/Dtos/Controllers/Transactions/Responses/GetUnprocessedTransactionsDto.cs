namespace FinanceManager.Domain.Dtos.Controllers.Transactions.Responses
{
    public class GetUnprocessedTransactionsDto
    {
        public required string Id { get; set; }
        public required string MerchantName { get; set; }
        public string? IconUrl { get; set; }
        public required string TransactionAmount { get; set; }
        public required string TransactionDate { get; set; }
    }
}
