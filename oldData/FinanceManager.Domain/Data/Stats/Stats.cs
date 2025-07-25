using BreganUtils;
using FinanceManager.Domain.Dtos.Stats;
using FinanceManagerAPI;
using FinanceManagerAPI.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Domain.Data.Stats
{
    public class Stats
    {
        public static async Task GetDailyStatsAndSendEmail()
        {
            using (var context = new DatabaseContext())
            {
                var stats = new DailyStatsDto
                {
                    UnprocessedTransactionsCount = await context.Transactions.Where(x => x.Processed == false).CountAsync(),
                    Transactions = await context.Transactions
                                    .Where(x => x.TransactionDate.Date == DateTime.UtcNow.Date)
                                    .Select(x => new Transaction
                                    {
                                        MerchantName = x.MerchantName,
                                        Amount = $"£{Math.Round(x.TransactionAmount / 100m, 2)}"
                                    })
                                    .ToArrayAsync(),
                    Pots = await context.Pots
                                .Where(x => x.Deleted == false && x.IsSavingsPot == false)
                                .Select(x => new Pot
                                {
                                    PotName = x.PotName,
                                    AmountLeft = $"£{Math.Round(x.PotAmountLeft / 100m, 2)}",
                                    AmountSpent = $"£{Math.Round(x.PotAmountSpent / 100m, 2)}"
                                })
                                .ToArrayAsync()
                };

                // Send the email
                MessageHelper.SendEmail(AppConfig.MMSApiKey, AppConfig.ToEmailAddress, AppConfig.ToEmailAddressName, AppConfig.FromEmailAddress, AppConfig.FromEmailAddressName, stats, AppConfig.DailyStatsTemplateId);
            }
        }
    }
}
