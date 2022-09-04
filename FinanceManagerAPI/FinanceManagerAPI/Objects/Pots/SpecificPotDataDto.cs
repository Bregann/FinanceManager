namespace FinanceManagerAPI.Objects.Pots
{
    public class SpecificPotDataDto
    {
        public int PotId { get; set; }

        public string? PotName { get; set; }
        public bool? Success { get; set; }
        public string? Error { get; set; }
        public decimal? PotAmount { get; set; }
    }
}
