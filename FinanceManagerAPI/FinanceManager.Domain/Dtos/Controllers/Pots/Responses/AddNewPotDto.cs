using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Controllers.Pots.Responses
{
    public class AddNewPotDto
    {
        public required bool Success {  get; set; }
        public required string Reason { get; set; }
        public int? PotId { get; set; }
    }
}
