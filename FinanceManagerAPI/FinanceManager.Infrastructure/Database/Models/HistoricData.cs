using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FinanceManager.Infrastructure.Database.Models
{
    public class HistoricData
    {
        [Key]
        public int Id { get; set; }
        public required DateTime Month { get; set; }
        public required decimal MonthlyIncome { get; set; }
        public required decimal AmountSaved {  get; set; }
        public required decimal AmountSpent { get; set; }
    }
}
