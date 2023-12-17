using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManager.Infrastructure.Database.Models
{
    public class HistoricData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required DateTime MonthStart { get; set; }
        public required DateTime MonthEnd { get; set; }
        public required decimal MonthlyIncome { get; set; }
        public required decimal AmountSaved { get; set; }
        public required decimal AmountSpent { get; set; }
    }
}
