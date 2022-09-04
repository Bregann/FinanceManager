using FinanceManagerAPI.Data.MonzoApi;
using Quartz;
using Quartz.Impl;
using Serilog;
using System.Reflection.Metadata;

namespace FinanceManagerAPI.Services
{
    public class JobScheduler
    {
        private static StdSchedulerFactory _factory;
        private static IScheduler _scheduler;

        public static async Task SetupJobScheduler()
        {
            //construct the scheduler factory
            _factory = new StdSchedulerFactory();
            _scheduler = await _factory.GetScheduler();
            await _scheduler.Start();

            var refreshTokenTrigger = TriggerBuilder.Create().WithIdentity("refreshTokenTrigger").WithCronSchedule("0 45 * ? * *").Build();
            var refreshToken = JobBuilder.Create<RefreshTokenJob>().WithIdentity("refreshToken").Build();

            var getAndProcessTransactionsTrigger = TriggerBuilder.Create().WithIdentity("getAndProcessTransactionsTrigger").WithCronSchedule("0 0 * ? * *").Build();
            var getAndProcessTransactions = JobBuilder.Create<GetAndProcessTransactionsJob>().WithIdentity("getAndProcessTransactions").Build();

            await _scheduler.ScheduleJob(refreshToken, refreshTokenTrigger);
            await _scheduler.ScheduleJob(getAndProcessTransactions, getAndProcessTransactionsTrigger);
            Log.Information("[Job Scheduler] Job Scheduler Setup");
        }
    }

    internal class RefreshTokenJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await MonzoApi.RefreshToken();
        }
    }

    internal class GetAndProcessTransactionsJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            //Get the new transactions
            await MonzoApi.GetTransactionsAndAddToDatabase();
        }
    }
}
