using System.ComponentModel.DataAnnotations;

namespace FinanceManager.Infrastructure.Database.Models
{
    public class HistoricData
    {
        [Key]
        public int Id { get; set; }
        public required DateTime MonthStart { get; set; }
        public required DateTime MonthEnd { get; set; }
        public required decimal MonthlyIncome { get; set; }
        public required decimal AmountSaved { get; set; }
        public required decimal AmountSpent { get; set; }
    }
}
