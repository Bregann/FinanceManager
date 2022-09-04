namespace FinanceManagerAPI.Dtos.Transactions
{
    public class TransactionsDto
    {
        public string Id { get; set; }
        public string ImgUrl { get; set; }
        public string TransactionDate { get; set; }
        public string MerchantName { get; set; }
        public string TransactionAmount { get; set; }
    }
}
