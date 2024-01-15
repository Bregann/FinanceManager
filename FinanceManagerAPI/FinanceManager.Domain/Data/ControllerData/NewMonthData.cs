using FinanceManager.Infrastructure.Database.Models;
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
                var pots = await context.Pots.Where(x => !x.Deleted).ToArrayAsync();
                var incomeInPence = (long)income * 100;

                //Update the last month if there is a month in there (won't be for first use)
                if (context.HistoricData.Any())
                {
                    var monthlyData = await context.HistoricData.OrderByDescending(x => x.Id).LastAsync();                  
                    monthlyData.MonthEnd = DateTime.UtcNow;
                    monthlyData.AmountSpent = pots.Sum(x => x.PotAmountSpent);

                    await context.SaveChangesAsync();
                }

                var spareMoney = incomeInPence - pots.Sum(x => x.PotAmount);

                //Reset the pots
                foreach (var pot in pots)
                {
                    //Add the extra money into the savings
                    if (pot.IsSavingsPot)
                    {
                        pot.PotAmountLeft += pot.PotAmount; //Pot amount is how much monthly to allocate
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

                //Add in the new month to the historic data
                await context.HistoricData.AddAsync(new HistoricData
                {
                    MonthStart = DateTime.UtcNow,
                    MonthEnd = new DateTime(),
                    AmountSaved = pots.Where(x => x.IsSavingsPot).Sum(x => x.PotAmount),
                    AmountSpent = 0,
                    MonthlyIncome = incomeInPence
                });

                await context.SaveChangesAsync();
            }

            return true;
        }
    }
}
