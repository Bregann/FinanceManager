using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Infrastructure.Database.Models
{
    public class Pots
    {
        [Key]
        public int Id { get; set; }
        public required bool Deleted { get; set; }
        public required string PotName { get; set; }
        public required long PotAmount { get; set; }
        public required long PotAmountSpent { get; set; }
        public required long PotAmountLeft { get; set; }
        public required bool IsSavingsPot { get; set; }
    }
}
