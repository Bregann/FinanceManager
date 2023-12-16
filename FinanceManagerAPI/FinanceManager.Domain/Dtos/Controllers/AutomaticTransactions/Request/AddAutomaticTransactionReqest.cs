using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Request
{
    public class AddAutomaticTransactionReqest
    {
        public required int PotId { get; set; }
        public required string MerchantName {  get; set; }
    }
}
