using FinanceManager.Domain.Dtos.Controllers.Home;
using FinanceManagerAPI.Database.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

                var culture = CultureInfo.GetCultureInfo("en-GB");

                return new GetHomeStatsDto
                {
                    MoneyIn = $"£{Math.Round((monthlyData.MonthlyIncome / 100m), 2).ToString("N", culture)}",
                    MoneyLeft = $"£{Math.Round((moneyLeft / 100m), 2).ToString("N", culture)}",
                    MoneySpent = $"£{Math.Round((moneySpent / 100m), 2).ToString("N", culture)}",
                    TotalSavings = $"£{Math.Round((totalSavings / 100m), 2).ToString("N", culture)}"
                };
            }
        }
    }
}
