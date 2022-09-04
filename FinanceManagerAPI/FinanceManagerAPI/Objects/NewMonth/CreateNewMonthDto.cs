namespace FinanceManagerAPI.Objects.NewMonth
{
    public class CreateNewMonthDto
    {
        public decimal Income { get; set; }
        public bool? Success { get; set; }
        public string? Error { get; set; }
    }
}
