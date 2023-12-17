using FinanceManager.Domain.Data.ControllerData;
using FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Request;
using FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Responses;
using FinanceManager.Domain.Dtos.Controllers.Shared.Response;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AutomaticTransactionsController
    {
        [HttpGet]
        public async Task<AutomaticTransactionDto[]> GetAutomaticTransactions()
        {
            return await AutomaticTransactionsData.GetAutomaticTransactions();
        }

        [HttpPost]
        public async Task<AddAutomaticTransactionDto> AddAutomaticTransaction([FromBody] AddAutomaticTransactionReqest request)
        {
            return await AutomaticTransactionsData.AddAutomaticTransaction(request);
        }

        [HttpPost]
        public async Task<BoolReasonDto> UpdateAutomaticTransaction([FromBody] UpdateAutomaticTransactionRequest request)
        {
            return await AutomaticTransactionsData.UpdateAutomaticTransaction(request);
        }
    }
}
