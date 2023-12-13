using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Infrastructure.Database.Models
{
    public class Pots
    {
        [Key]
        public int Id { get; set; }
        public required bool Deleted { get; set; }
        public required string PotName { get; set; }
        public required decimal PotAmount { get; set; }
        public required decimal PotAmountSpent { get; set; }
        public required decimal PotAmountLeft { get; set; }
        public required bool IsSavingsPot { get; set; }
    }
}
