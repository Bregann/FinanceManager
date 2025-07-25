namespace FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Responses
{
    public class AutomaticTransactionDto
    {
        public required int Id { get; set; }
        public required string MerchantName { get; set; }
        public required string PotId { get; set; }
    }
}
