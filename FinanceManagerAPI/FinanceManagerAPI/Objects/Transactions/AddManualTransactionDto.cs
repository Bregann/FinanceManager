namespace FinanceManagerAPI.Objects.Transactions
{
    public class AddManualTransactionDto
    {
        public decimal TransactionAmount { get; set; }
        public long PotId { get; set; }
    }
}
