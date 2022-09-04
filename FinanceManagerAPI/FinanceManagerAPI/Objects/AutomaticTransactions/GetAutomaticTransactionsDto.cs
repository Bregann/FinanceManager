namespace FinanceManagerAPI.Objects.AutomaticTransactions
{
    public class GetAutomaticTransactionsDto
    {
        public List<AutomaticTransationItem> AutomaticTransactions { get; set; }
    }

    public class AutomaticTransationItem
    {
        public string Name { get; set; }
        public string PotName { get; set; }
    }
}
