using FinanceManagerAPI.Database.Context;
using FinanceManagerAPI.Dtos.Transactions;
using FinanceManagerAPI.Objects.Transactions;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [HttpGet("GetUnprocessedTransactions")]
        public ActionResult<IEnumerable<TransactionsDto>> GetUnprocessedTransactions()
        {
            using(var context = new DatabaseContext())
            {
                var transactions = context.Transactions.Where(x => !x.Processed).ToList();

                //If there's no transactions then return not found as the js will handle that
                if (transactions.Count == 0)
                {
                    return NotFound();
                }

                var transactionDto = new List<TransactionsDto>();

                foreach(var transaction in transactions)
                {
                    var transactionTime = transaction.TransactionDate.ToString("dd MMMM yyyy") + " at " + transaction.TransactionDate.ToString("HH:MMtt");

                    transactionDto.Add(new TransactionsDto
                    {
                        Id = transaction.Id,
                        ImgUrl = transaction.ImgUrl,
                        TransactionAmount = $"£{transaction.TransactionAmount}",
                        TransactionDate = transactionTime,
                        MerchantName = transaction.MerchantName
                    });
                }

                return transactionDto;
            }
        }

        [HttpPost("UpdateTransaction")]
        public bool UpdateTransaction([FromBody] UpdateTransactionDto dto)
        {
            using(var context = new DatabaseContext())
            {
                var transaction = context.Transactions.Where(x => x.Id == dto.TransactionId).FirstOrDefault();
                var pot = context.Pots.Where(x => x.PotId == dto.PotId).FirstOrDefault();

                //if either the transaction or pot doesnt exist then don't run
                if (transaction == null || pot == null)
                {
                    return false;
                }

                transaction.PotId = pot.PotId;
                transaction.Processed = true;
                pot.AmountLeftThisMonth -= transaction.TransactionAmount;

                context.SaveChanges();
                return true;
            }
        }

        [HttpPost("AddManualTransaction")]
        public bool AddManualTransaction([FromBody] AddManualTransactionDto dto)
        {
            if (decimal.Round(dto.TransactionAmount, 2) != dto.TransactionAmount)
            {
                return false;
            }

            using(var context = new DatabaseContext())
            {
                context.Transactions.Add(new Database.Models.Transactions
                {
                    TransactionDate = DateTime.Now.ToUniversalTime(),
                    Id = DateTime.Now.ToUniversalTime().ToString(),
                    ImgUrl = "n/a",
                    TransactionAmount = dto.TransactionAmount,
                    MerchantName = "n/a",
                    PotId = dto.PotId,
                    Processed = true
                });

                var pot = context.Pots.Where(x => x.PotId == dto.PotId).First();
                pot.AmountLeftThisMonth -= dto.TransactionAmount;

                context.SaveChanges();
                return true;
            }
        }
    }
}
