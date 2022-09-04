namespace FinanceManagerAPI.Objects.Pots
{
    public class EditPotDto
    {
        public int PotId { get; set; }
        public string PotName { get; set; }
        public long PotAmount { get; set; }
        public bool NameChanged { get; set; }
        public bool? Success { get; set; }
        public string? Error { get; set; }
    }
}
