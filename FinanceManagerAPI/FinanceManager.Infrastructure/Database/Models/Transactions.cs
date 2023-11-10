using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManagerAPI.Database.Models
{
    public class Transactions
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("processed")]
        public bool Processed { get; set; }

        [Column("imgUrl")]
        public string ImgUrl { get; set; }

        [Column("transactionDate")]
        public DateTime TransactionDate { get; set; }

        [Column("merchantName")]
        public string MerchantName { get; set; }

        [Column("transactionAmount")]
        public decimal TransactionAmount { get; set; }

        [Column("potId")]
        public long PotId { get; set; }
    }
}
