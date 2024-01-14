using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewMonthController
    {
        [HttpPost]
        public async Task<bool> AddNewMonth()
        {

        }
    }
}
