namespace FinanceManager.Domain.Dtos.Controllers.Pots.Requests
{
    public class UpdatePotRequest
    {
        public required int PotId { get; set; }
        public required string PotName { get; set; }
        public required decimal PotAmount { get; set; }
    }
}
