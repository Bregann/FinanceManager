using BreganUtils;
using FinanceManager.Domain.Dtos.Controllers.Transactions.Responses;
using FinanceManagerAPI.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Domain.Data.ControllerData
{
    public class TransactionsData
    {
        public static async Task<GetUnprocessedTransactionsDto[]> GetUnprocessedTransactionsFromDatabase()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Transactions.Where(x => x.Processed == false).Select(x => new GetUnprocessedTransactionsDto
                {
                    Id = x.Id,
                    TransactionAmount = x.TransactionAmount,
                    IconUrl = x.ImgUrl,
                    MerchantName = x.MerchantName,
                    TransactionDate = DateTimeHelper.HumanizeDateTime(x.TransactionDate),
                }).ToArrayAsync();
            }
        }
    }
}
