namespace FinanceManagerAPI.Objects.MonthlyBreakdown
{
    public class GetMonthlyBreakdownTableDataDto
    {
        public List<MonthlyBreakdownTableData> MoneySpentPerPotData { get; set; }
        public List<MonthlyBreakdownTableData> TopPlacesMoneySpentData { get; set; }
        public List<MonthlyBreakdownTableData> MostExpensivePurchasesData { get; set; }
    }

    public class MonthlyBreakdownTableData
    {
        public string Name { get; set; }
        public string Amount { get; set; }
    }
}
