using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Controllers.Transactions.Requests
{
    public class UpdateTransactionRequest
    {
        public required string TransactionId { get; set; }
        public required int PotId { get; set; }
    }
}
