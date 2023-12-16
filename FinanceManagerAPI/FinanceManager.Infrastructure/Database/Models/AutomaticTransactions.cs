using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable CS8618

namespace FinanceManager.Infrastructure.Database.Models
{
    public class AutomaticTransactions
    {
        [Key]
        public int Id { get; set; }
        public required string MerchantName { get; set; }

        [ForeignKey("Pots")]
        public required int PotId { get; set; }

        public virtual Pots Pot { get; set; }
    }
}
