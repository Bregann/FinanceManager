using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManagerAPI.Database.Models
{
    public class AutomaticTransactions
    {
        [Key]
        [Column("name")]
        public string Name { get; set; }

        [Column("potId")]
        public int PotId { get; set; }
    }
}
