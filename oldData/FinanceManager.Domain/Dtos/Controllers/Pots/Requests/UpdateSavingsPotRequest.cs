using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Controllers.Pots.Requests
{
    public class UpdateSavingsPotRequest
    {
        public int PotId { get; set; }
        public double SavingsAmount { get; set; }
    }
}
