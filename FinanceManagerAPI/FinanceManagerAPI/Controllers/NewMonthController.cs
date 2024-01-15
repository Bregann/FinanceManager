using FinanceManager.Domain.Data.ControllerData;
using FinanceManager.Domain.Dtos.Controllers.NewMonth.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewMonthController
    {
        [HttpPost]
        public async Task<bool> AddNewMonth([FromBody] AddNewMonth income)
        {
            return await NewMonthData.AddNewMonth(income.Income);
        }
    }
}
