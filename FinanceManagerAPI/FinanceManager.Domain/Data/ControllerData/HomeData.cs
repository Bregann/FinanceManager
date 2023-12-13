using FinanceManager.Domain.Dtos.Controllers.Home;
using FinanceManagerAPI.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Domain.Data.ControllerData
{
    public class HomeData
    {
        public static async Task<GetHomeStatsDto> GetHomeData()
        {
            using (var context = new DatabaseContext())
            {
                var monthlyData = await context.HistoricData.OrderByDescending(x => x.Id).LastAsync();
                var moneyLeft = await context.Pots.Where(x => x.Deleted == false && x.IsSavingsPot == false).Select(x => x.PotAmountLeft).SumAsync();
                var moneySpent = await context.Pots.Where(x => x.Deleted == false && x.IsSavingsPot == false).Select(x => x.PotAmountSpent).SumAsync();
                var totalSavings = await context.Pots.Where(x => x.Deleted == false && x.IsSavingsPot == true).Select(x => x.PotAmount).SumAsync();

                return new GetHomeStatsDto
                {
                    MoneyIn = monthlyData.MonthlyIncome.ToString("N0"),
                    MoneyLeft = moneyLeft.ToString("N0"),
                    MoneySpent = moneySpent.ToString("N0"),
                    TotalSavings = totalSavings.ToString("N0")
                };
            }
        }
    }
}
