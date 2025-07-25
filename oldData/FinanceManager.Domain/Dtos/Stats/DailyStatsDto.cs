using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Stats
{
    public partial class DailyStatsDto
    {
        public required long UnprocessedTransactionsCount { get; set; }
        public required Transaction[] Transactions { get; set; }
        public required Pot[] Pots { get; set; }
    }

    public partial class Pot
    {
        public required string PotName { get; set; }
        public required string AmountSpent { get; set; }
        public required string AmountLeft { get; set; }
    }

    public partial class Transaction
    {
        public required string MerchantName { get; set; }
        public required string Amount { get; set; }
    }
}
