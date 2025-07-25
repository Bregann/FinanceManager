namespace FinanceManager.Domain.Dtos.Controllers.Pots.Responses
{
    public class GetPotsStatsDto
    {
        public required int PotId { get; set; }
        public required string PotName { get; set; }
        public required bool IsSavingsPot { get; set; }
        public required string AmountAllocated { get; set; }
        public required decimal AmountLeft { get; set; }
        public required string AmountSpent { get; set; }
    }
}
