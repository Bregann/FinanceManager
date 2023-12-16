using FinanceManager.Domain.Data.ControllerData;
using FinanceManager.Domain.Dtos.Controllers.Pots.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PotsController : ControllerBase
    {
        [HttpGet]
        public async Task<PotsDropdownValuesDto[]> GetPotDropdownValues()
        {
            return await PotsData.GetPotDropdownValues();
        }

        [HttpGet]
        public async Task<GetPotsStatsDto[]> GetPotsStats()
        {
            return await PotsData.GetPotsStats();
        }
    }
}
