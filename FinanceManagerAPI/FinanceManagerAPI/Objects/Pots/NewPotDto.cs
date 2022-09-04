namespace FinanceManagerAPI.Dtos.Pots
{
    public class NewPotDto
    {
        public string PotName { get; set; }
        public decimal PotAmount { get; set; }

        public bool? Success { get; set; }
        public string? Error { get; set; }
    }
}
