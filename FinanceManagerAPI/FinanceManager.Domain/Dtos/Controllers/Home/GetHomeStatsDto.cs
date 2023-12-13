namespace FinanceManager.Domain.Dtos.Controllers.Home
{
    public class GetHomeStatsDto
    {
        public required string MoneyIn { get; set; }
        public required string MoneySpent { get; set; }
        public required string MoneyLeft { get; set; }
        public required string TotalSavings { get; set; }
    }
}
