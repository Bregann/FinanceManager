using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Controllers.Pots.Requests
{
    public class AddNewPotRequest
    {
        public required string PotName { get; set; }
        public required decimal Amount { get; set; }
        public required bool IsSavingsPot { get; set; }
    }
}
