using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManagerAPI.Database.Models
{
    public class Pots
    {
        [Key]
        [Column("potId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PotId { get; set; }

        [Column("potName")]
        public string PotName { get; set; }

        [Column("amountAllocated")]
        public decimal AmountAllocated { get; set; }

        [Column("amountLeftThisMonth")]
        public decimal AmountLeftThisMonth { get; set; }
    }
}
