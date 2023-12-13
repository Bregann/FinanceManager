using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Infrastructure.Database.Models
{
    public class AutomaticTransactions
    {
        [Key]
        public int Id { get; set; }
        public required string MerchantName { get; set; }
        public virtual required Pots Pot { get; set; }
    }
}
