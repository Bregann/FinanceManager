using FinanceManager.Domain.Data.ControllerData;
using FinanceManager.Domain.Dtos.Controllers.Transactions.Requests;
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

        [HttpPost]
        public async Task<bool> UpdateTransaction([FromBody] UpdateTransactionRequest request)
        {
            return await TransactionsData.UpdateTransaction(request);
        }

        [HttpPost("{transactionId}")]
        public async Task<bool> DeleteTransaction([FromRoute] string transactionId)
        {
            return await TransactionsData.DeleteTransaction(transactionId);
        }
    }
}
