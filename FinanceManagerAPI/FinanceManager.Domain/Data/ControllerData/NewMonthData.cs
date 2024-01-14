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
        public static async Task<bool> AddNewMonth()
        {
            using (var context = new DatabaseContext())
            {
                var pots = await context.Pots.ToArrayAsync();

                //Update the last month if there is a month in there (won't be for first use)
                if (context.HistoricData.Any())
                {
                    var monthlyData = await context.HistoricData.OrderByDescending(x => x.Id).LastAsync();                  
                    monthlyData.MonthEnd = DateTime.UtcNow;
                    monthlyData.AmountSpent = pots.Sum(x => x.PotAmountSpent);
                }
            }
        }
    }
}
