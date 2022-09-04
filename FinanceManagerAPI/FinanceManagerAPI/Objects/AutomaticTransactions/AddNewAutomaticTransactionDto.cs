namespace FinanceManagerAPI.Objects.AutomaticTransactions
{
    public class AddNewAutomaticTransactionDto
    {
        public string TransactionName { get; set; }
        public int PotId { get; set; }

        public bool? Success { get; set; }
        public string? Error { get; set; }
    }
}