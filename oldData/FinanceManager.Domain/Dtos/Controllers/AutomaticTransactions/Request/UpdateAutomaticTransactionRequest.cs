namespace FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Request
{
    public class UpdateAutomaticTransactionRequest
    {
        public required int Id { get; set; }
        public required int PotId { get; set; }
        public required string MerchantName { get; set; }
    }
}
