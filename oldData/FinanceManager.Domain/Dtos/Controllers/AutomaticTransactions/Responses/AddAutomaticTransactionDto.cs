namespace FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Responses
{
    public class AddAutomaticTransactionDto
    {
        public required bool Success { get; set; }
        public required string Reason { get; set; }
        public int? AutomaticTransactionId { get; set; }
    }
}
