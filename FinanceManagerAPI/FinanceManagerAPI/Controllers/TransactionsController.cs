using FinanceManager.Domain.Data.ControllerData;
using FinanceManager.Domain.Dtos.Controllers.Transactions.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManager.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        [HttpGet]
        public async Task<GetUnprocessedTransactionsDto[]> GetUnprocessedTransactionsFromDatabase()
        {
            return await TransactionsData.GetUnprocessedTransactionsFromDatabase();
        }
    }
}
