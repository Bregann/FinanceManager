using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Controllers.Transactions.Responses
{
    public class GetProcessedTransactionsForMonthDto
    {
        public required string Id { get; set; }
        public required string MerchantName { get; set; }
        public string? IconUrl { get; set; }
        public required string? PotId { get; set; }
        public required string TransactionAmount { get; set; }
        public required string TransactionDate { get; set; }
    }
}
