using FinanceManagerAPI.Database.Context;
using FinanceManagerAPI.Objects.AutomaticTransactions;
using FinanceManagerAPI.Objects.Pots;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomaticTransactionsController : ControllerBase
    {
        [HttpPost("AddNewAutomaticTransaction")]
        public AddNewAutomaticTransactionDto AddNewAutomaticTransaction([FromBody] AddNewAutomaticTransactionDto dto)
        {
            if (dto.PotId == 0)
            {
                return new AddNewAutomaticTransactionDto
                {
                    Success = false,
                    Error = "Please select a pot option"
                };
            }

            using(var context = new DatabaseContext())
            {
                //Make sure it doesnt already exist
                var transaction = context.AutomaticTransactions.Where(x => x.Name.ToLower() == dto.TransactionName.ToLower()).FirstOrDefault();

                if (transaction != null)
                {
                    return new AddNewAutomaticTransactionDto
                    {
                        Success = false,
                        Error = "Automatic transaction already exists"
                    };
                }

                //Make sure the pot exists
                var pot = context.Pots.Where(x => x.PotId == dto.PotId).FirstOrDefault();

                if (pot == null)
                {
                    return new AddNewAutomaticTransactionDto
                    {
                        Success = false,
                        Error = "Invalid pot ID"
                    };
                }

                context.AutomaticTransactions.Add(new Database.Models.AutomaticTransactions { Name = dto.TransactionName, PotId = dto.PotId });
                context.SaveChanges();

                return new AddNewAutomaticTransactionDto{ Success = true };
            }
        }

        [HttpGet("GetAutomaticTransactions")]
        public ActionResult<GetAutomaticTransactionsDto> GetAutomaticTransactions()
        {
            using(var context = new DatabaseContext())
            {
                //Get the pots and convert to dictionary for mapping to name
                var pots = context.Pots.ToDictionary(x => x.PotId, x => x.PotName);
                var automaticTransactions = context.AutomaticTransactions.ToList();

                if (automaticTransactions.Count == 0)
                {
                    return NotFound();
                }

                var transactionListDto = new List<AutomaticTransationItem>();

                //Map all the transactions to the dto
                foreach (var transaction in automaticTransactions)
                {
                    transactionListDto.Add(new AutomaticTransationItem
                    {
                        Name = transaction.Name,
                        PotName = pots[transaction.PotId]
                    });
                }

                return new GetAutomaticTransactionsDto { AutomaticTransactions = transactionListDto };
            }
        }

        [HttpDelete("DeleteAutomaticTransaction")]
        public IActionResult DeleteAutomaticTransaction(DeleteAutomaticTransactionDto dto)
        {
            using (var context = new DatabaseContext())
            {
                var transaction = context.AutomaticTransactions.Where(x => x.Name == dto.Name).FirstOrDefault();

                if (transaction == null)
                {
                    return NotFound();
                }
                else
                {
                    context.AutomaticTransactions.Remove(transaction);
                    context.SaveChanges();
                    return Ok();
                }
            }
        }
    }
}
