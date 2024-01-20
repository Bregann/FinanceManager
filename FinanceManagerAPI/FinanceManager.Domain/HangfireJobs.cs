using FinanceManagerAPI.Data.MonzoApi;
using Hangfire;
using Serilog;

namespace FinanceManager.Domain
{
    public class HangfireJobs
    {
        public static void SetupHangfireJobs()
        {
            RecurringJob.AddOrUpdate("RefreshApi", () => RefreshMonzoApi(), "45 * * * *");
            RecurringJob.AddOrUpdate("UpdateMonzoTransactions", () => UpdateMonzoTransactions(), "0 * * * *");

            Log.Information("[Job Scheduler] Hangfire Jobs Setup");
        }

        public static async Task RefreshMonzoApi()
        {
            await MonzoApi.RefreshToken();
        }

        public static async Task UpdateMonzoTransactions()
        {
            await MonzoApi.GetTransactionsAndAddToDatabase();
            await MonzoApi.UpdateAutomaticTransactions();
        }
    }
}
