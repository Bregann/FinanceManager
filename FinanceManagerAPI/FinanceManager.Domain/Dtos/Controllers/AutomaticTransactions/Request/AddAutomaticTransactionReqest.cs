namespace FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Request
{
    public class AddAutomaticTransactionReqest
    {
        public required int PotId { get; set; }
        public required string MerchantName { get; set; }
    }
}
