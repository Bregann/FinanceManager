using FinanceManager.Domain.DTOs.Banking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Interfaces.Helpers
{
    public interface IBankApiHelper
    {
        Task<string?> GetGoCardlessBankingDataAccessToken();
        Task<GoCardlessTransactionsResponse?> GetGoCardlessBankingDataTransactions(string accessToken, string accountName);
        Task<List<Transaction>?> GetMonzoTransactions();
        Task RefreshMonzoToken();
    }
}
