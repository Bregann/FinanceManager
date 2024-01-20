namespace FinanceManager.Domain.Dtos.Controllers.Pots.Responses
{
    public class GetPotListDto
    {
        public required int PotId { get; set; }
        public required string PotName { get; set; }
        public required decimal PotAmount { get; set; }
        public required bool IsSavingsPot { get; set; }
    }
}
