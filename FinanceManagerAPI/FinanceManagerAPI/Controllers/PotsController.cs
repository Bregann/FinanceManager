using FinanceManager.Domain.Data.ControllerData;
using FinanceManager.Domain.Dtos.Controllers.Pots.Requests;
using FinanceManager.Domain.Dtos.Controllers.Pots.Responses;
using FinanceManager.Domain.Dtos.Controllers.Shared.Response;
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

        [HttpGet]
        public async Task<GetPotListDto[]> GetPotList()
        {
            return await PotsData.GetPotList();
        }

        [HttpPost]
        public async Task<AddNewPotDto> AddNewPot([FromBody] AddNewPotRequest request)
        {
            return await PotsData.AddNewPot(request);
        }

        [HttpPost]
        public async Task<BoolReasonDto> UpdatePot([FromBody] UpdatePotRequest request)
        {
            return await PotsData.UpdatePot(request);
        }
    }
}
