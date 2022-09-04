namespace FinanceManagerAPI.Objects.NewMonth
{
    public class GetAddMonthBreakdownDto
    {
        public decimal Income { get; set; }

        public bool? Success { get; set; }
        public string? Error { get; set; }
        public List<Pot>? Pots { get; set; }
        public string? Total { get; set; }
        public string? SpareMoney { get; set; }
    }

    public class Pot
    {
        public string PotName { get; set; }
        public string AmountAllocated { get; set; }
    }
}
