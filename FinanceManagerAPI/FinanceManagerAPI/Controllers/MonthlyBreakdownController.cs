using FinanceManagerAPI.Database.Context;
using FinanceManagerAPI.Objects.MonthlyBreakdown;
using FinanceManagerAPI.Objects.NewMonth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FinanceManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyBreakdownController : ControllerBase
    {
        [HttpGet("GetMonthlyBreakdownTableData")]
        public GetMonthlyBreakdownTableDataDto GetMonthlyBreakdownTableData()
        {
            using(var context = new DatabaseContext())
            {
                //Get the pot data
                var potsData = context.Pots.ToList().Select(x => new MonthlyBreakdownTableData { Name = x.PotName, Amount = $"£{x.AmountAllocated - x.AmountLeftThisMonth}" }).ToList();

                //Get the transactions during the month added. Months are added on 25th of each month
                var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 25).ToUniversalTime();
                var transactions = context.Transactions.Where(x => x.TransactionDate > dt).OrderByDescending(x => x.TransactionAmount).ToList(); //order it by transaction amount to make it easier getting the top purchases

                //Take the top 10 and convert that into a list
                var highestTransactionData = transactions.Take(10).Select(x => new MonthlyBreakdownTableData { Name = x.MerchantName, Amount = $"£{x.TransactionAmount}" }).ToList();

                //Loop through the rest of the transactions and put into a dict
                var topLocationsDecimalDict = new Dictionary<string, decimal>();
                foreach (var transaction in transactions)
                {
                    //if the location is already in the dict then add the value on
                    if (topLocationsDecimalDict.ContainsKey(transaction.MerchantName))
                    {
                        topLocationsDecimalDict[transaction.MerchantName] += transaction.TransactionAmount;
                    }
                    else
                    {
                        topLocationsDecimalDict.Add(transaction.MerchantName, transaction.TransactionAmount);
                    }
                }

                //convert the top locations into the dto data
                var topLocationsData = topLocationsDecimalDict.OrderByDescending(x => x.Value).Take(10).Select(x => new MonthlyBreakdownTableData { Name = x.Key, Amount = $"£{x.Value}"}).ToList();

                return new GetMonthlyBreakdownTableDataDto { MoneySpentPerPotData = potsData, MostExpensivePurchasesData = highestTransactionData, TopPlacesMoneySpentData = topLocationsData };
            }
        }

        [HttpGet("GetMonthlyBreakdownTableChartData")]
        public GetMonthlyBreakdownTableChartDataDto GetMonthlyBreakdownTableChartData()
        {
            using (var context = new DatabaseContext())
            {
                //Get the pot data
                var potsData = context.Pots.ToList().Select(x => new MonthlyBreakdownTableChartData { Name = x.PotName, Amount = x.AmountAllocated - x.AmountLeftThisMonth }).ToList();

                //Get the transactions during the month added. Months are added on 25th of each month
                var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 25).ToUniversalTime();
                var transactions = context.Transactions.Where(x => x.TransactionDate >= dt).OrderByDescending(x => x.TransactionAmount).ToList(); //order it by transaction amount to make it easier getting the top purchases

                //Take the top 10 and convert that into a list
                var highestTransactionData = transactions.Take(10).Select(x => new MonthlyBreakdownTableChartData { Name = x.MerchantName, Amount = x.TransactionAmount }).ToList();

                //Loop through the rest of the transactions and put into a dict
                var topLocationsDecimalDict = new Dictionary<string, decimal>();
                foreach (var transaction in transactions)
                {
                    //if the location is already in the dict then add the value on
                    if (topLocationsDecimalDict.ContainsKey(transaction.MerchantName))
                    {
                        topLocationsDecimalDict[transaction.MerchantName] += transaction.TransactionAmount;
                    }
                    else
                    {
                        topLocationsDecimalDict.Add(transaction.MerchantName, transaction.TransactionAmount);
                    }
                }

                //convert the top locations into the dto data
                var topLocationsData = topLocationsDecimalDict.OrderByDescending(x => x.Value).Take(10).Select(x => new MonthlyBreakdownTableChartData { Name = x.Key, Amount = x.Value }).ToList();

                return new GetMonthlyBreakdownTableChartDataDto { MoneySpentPerPotData = potsData, MostExpensivePurchasesData = highestTransactionData, TopPlacesMoneySpentData = topLocationsData };
            }
        }

        [HttpGet("GetMoneySpentPerDayChartData")]
        public GetMoneySpentPerDayChartDataDto GetMoneySpentPerDayChartData()
        {
            //Generate every datetime between the two dates for the graph
            var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 26).ToUniversalTime();
            var monthEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25).ToUniversalTime();

            var dates = new List<DateTime>();

            for (var dt = monthStart; dt <= monthEnd; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }


            using (var context = new DatabaseContext())
            {
                //Get the transactions during the month added. Months are added on 25th of each month
                var dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 25).ToUniversalTime();
                var transactions = context.Transactions.Where(x => x.TransactionDate >= monthStart).OrderByDescending(x => x.TransactionDate).ToList(); //order it by transaction amount to make it easier getting the top purchases

                var dtoTransactionList = new List<MoneySpentPerDayChartData>();
                //Loop through all the dates and add the transactions totals into the dto
                foreach(var date in dates)
                {
                    var transactionsOnDay = transactions.Where(x => x.TransactionDate.Date == date.Date).ToList();

                    //if there's no transactions then add in 0
                    if(transactionsOnDay.Count == 0)
                    {
                        dtoTransactionList.Add(new MoneySpentPerDayChartData { DayMonth = date.ToString("dd/MM"), Amount = 0m});
                    }
                    else
                    {
                        dtoTransactionList.Add(new MoneySpentPerDayChartData { DayMonth = date.ToString("dd/MM"), Amount = transactionsOnDay.Sum(x => x.TransactionAmount) });
                    }
                }

                return new GetMoneySpentPerDayChartDataDto { Data = dtoTransactionList};
            }
        }
    }
}
