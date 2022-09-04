namespace FinanceManagerAPI.Objects.MonthlyBreakdown
{
    public class GetMonthlyBreakdownTableChartDataDto
    {
        public List<MonthlyBreakdownTableChartData> MoneySpentPerPotData { get; set; }
        public List<MonthlyBreakdownTableChartData> TopPlacesMoneySpentData { get; set; }
        public List<MonthlyBreakdownTableChartData> MostExpensivePurchasesData { get; set; }
    }

    public class MonthlyBreakdownTableChartData
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
