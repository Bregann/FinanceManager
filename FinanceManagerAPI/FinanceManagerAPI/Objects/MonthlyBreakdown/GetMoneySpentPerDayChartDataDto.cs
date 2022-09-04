namespace FinanceManagerAPI.Objects.MonthlyBreakdown
{
    public class GetMoneySpentPerDayChartDataDto
    {
        public List<MoneySpentPerDayChartData> Data { get; set; }
    }

    public class MoneySpentPerDayChartData
    {
        public string DayMonth { get; set; }
        public decimal Amount { get; set; }
    }
}
