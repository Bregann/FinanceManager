using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Responses
{
    public class AddAutomaticTransactionDto
    {
        public required bool Success { get; set; }
        public required string Reason { get; set; }
        public int? AutomaticTransactionId { get; set; }
    }
}
