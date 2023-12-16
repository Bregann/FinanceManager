using BreganUtils;
using FinanceManager.Domain.Dtos.Controllers.Transactions.Requests;
using FinanceManager.Domain.Dtos.Controllers.Transactions.Responses;
using FinanceManagerAPI.Database.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
                    TransactionAmount = (x.TransactionAmount / 100m).ToString("C"),
                    IconUrl = x.ImgUrl,
                    MerchantName = x.MerchantName,
                    TransactionDate = DateTimeHelper.HumanizeDateTime(x.TransactionDate)
                }).ToArrayAsync();
            }
        }

        public static async Task<bool> UpdateTransaction(UpdateTransactionRequest request)
        {
            using (var context = new DatabaseContext())
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.TransactionId);
                var pot = await context.Pots.FirstOrDefaultAsync(x => x.Id == request.PotId);

                if (transaction == null || pot == null)
                {
                    Log.Information("[Transactions] Invalid request - transaction or pot was null");
                    return false;
                }

                //Check if there is a pot already assigned to the transaction
                if (transaction.PotId != null)
                {
                    //Add back the money into the pot
                    transaction.Pot!.PotAmountLeft += transaction.TransactionAmount;
                    transaction.Pot!.PotAmountSpent -= transaction.TransactionAmount;
                    await context.SaveChangesAsync();

                    Log.Information("[Transactions] Old pot updated");
                }

                transaction.Pot = pot;
                transaction.Processed = true;
                pot.PotAmountLeft -= transaction.TransactionAmount;
                pot.PotAmountSpent += transaction.TransactionAmount;

                await context.SaveChangesAsync();
                Log.Information("[Transactions] Pot and transaction updated");
                return true;
            }
        }
    }
}
