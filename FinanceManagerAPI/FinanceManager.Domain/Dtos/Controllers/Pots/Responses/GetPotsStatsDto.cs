using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Controllers.Pots.Responses
{
    public class GetPotsStatsDto
    {
        public required int PotId { get; set; }
        public required string PotName { get; set; }
        public required bool IsSavingsPot { get; set; }
        public required string AmountAllocated { get; set; }
        public required string AmountLeft { get; set; }
        public required string AmountSpent { get; set; }
    }
}
