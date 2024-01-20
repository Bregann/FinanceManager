using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManager.Infrastructure.Database.Models
{
    public class Pots
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required bool Deleted { get; set; }
        public required string PotName { get; set; }
        public required long PotAmount { get; set; }
        public required long PotAmountSpent { get; set; }
        public required long PotAmountLeft { get; set; }
        public required bool IsSavingsPot { get; set; }
    }
}
