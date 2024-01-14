using FinanceManagerAPI.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Data.ControllerData
{
    public class NewMonthData
    {
        public static async Task<bool> AddNewMonth(double income)
        {
            using (var context = new DatabaseContext())
            {
                var pots = await context.Pots.ToArrayAsync();
                var incomeInPence = income * 100;

                //Update the last month if there is a month in there (won't be for first use)
                if (context.HistoricData.Any())
                {
                    var monthlyData = await context.HistoricData.OrderByDescending(x => x.Id).LastAsync();                  
                    monthlyData.MonthEnd = DateTime.UtcNow;
                    monthlyData.AmountSpent = pots.Sum(x => x.PotAmountSpent);

                    await context.SaveChangesAsync();
                }

                var spareMoney = (long)incomeInPence - pots.Sum(x => x.PotAmount);

                //Reset the pots
                foreach (var pot in pots)
                {
                    //don't touch the savings pots
                    if (pot.IsSavingsPot)
                    {
                        continue;
                    }

                    pot.PotAmountSpent = 0;

                    //Pot id 1 = spare money
                    if (pot.Id == 1)
                    {
                        pot.PotAmountLeft = spareMoney;
                        continue;
                    }

                    pot.PotAmountLeft = pot.PotAmount;
                }

                await context.SaveChangesAsync();
            }

            return true;
        }
    }
}
