using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Dtos.Controllers.Shared.Response
{
    public class BoolReasonDto
    {
        public required bool Success { get; set; }
        public required string Reason { get; set; }
    }
}
