using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Responses
{
    public class AutomaticTransactionDto
    {
        public required int Id { get; set; }
        public required string MerchantName { get; set; }
        public required string PotId { get; set; }
    }
}
