using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManagerAPI.Database.Models
{
    public class Transactions
    {
        [Key]
        public required string Id { get; set; }
        public required bool Processed { get; set; }
        public string? ImgUrl { get; set; }
        public required DateTime TransactionDate { get; set; }
        public required string MerchantName { get; set; }
        public required decimal TransactionAmount { get; set; }
    }
}
