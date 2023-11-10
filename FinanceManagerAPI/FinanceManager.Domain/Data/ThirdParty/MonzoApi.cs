using FinanceManager.Domain.Dtos.ThirdParty;
using FinanceManagerAPI.Database.Context;
using FinanceManagerAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using Serilog;

namespace FinanceManagerAPI.Data.MonzoApi
{
    public class MonzoApi
    {
        public static async Task RefreshToken()
        {
            //Create the request 
            var client = new RestClient("https://api.monzo.com/");
            var request = new RestRequest($"/oauth2/token", Method.Post);
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("client_id", AppConfig.ClientId);
            request.AddParameter("client_secret", AppConfig.ClientSecret);
            request.AddParameter("refresh_token", AppConfig.RefreshToken);


            var response = await client.ExecuteAsync(request);
            if (response.Content == "")
            {
                Log.Warning("[Monzo Refresh] Refresh Error");
                return;
            }

            var transactionsResult = JsonConvert.DeserializeObject<MonzoRefreshResponse>(response.Content);
            AppConfig.UpdateAccessAndRefreshToken(transactionsResult.AccessToken, transactionsResult.RefreshToken);

            Log.Information("[Monzo Refresh Job] Monzo Refreshed");
        }

        public static async Task GetTransactionsAndAddToDatabase()
        {
            //Create the request
            var client = new RestClient("https://api.monzo.com/");
            var request = new RestRequest($"/transactions?expand[]=merchant&account_id={AppConfig.AccountId}&since={DateTime.Now.AddDays(-7).ToString("yyyy-MM-ddTh:mm:ssZ")}", Method.Get);
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(AppConfig.AccessToken, "Bearer");

            //Get the response and Deserialize
            var response = await client.ExecuteAsync(request);

            if (response.Content == "")
            {
                Log.Warning("[Monzo Transactions Update] No response found");
                return;
            }

            var transactionsResult = JsonConvert.DeserializeObject<MonzoDataResponse>(response.Content);

            if (transactionsResult.Transactions == null)
            {
                Log.Information("[Monzo Transactions Update] No transactions found");
                return;
            }

            using (var context = new DatabaseContext())
            {
                //loop through the transactions and insert them to the db
                var transactionListToDb = new List<Transactions>();
                foreach (var transaction in transactionsResult.Transactions)
                {
                    //Check if it already exists, if it does then dont insert
                    var dbCheck = await context.Transactions.Where(x => x.Id == transaction.Id).FirstOrDefaultAsync();

                    if (dbCheck != null)
                    {
                        continue;
                    }
                
                    //Convert the amount to a positive
                    var positiveTransactionAmount = Math.Abs(transaction.Amount);

                    if(transaction.Merchant == null)
                    {
                        continue;
                    }

                    transactionListToDb.Add(new Transactions
                    {
                        Id = transaction.Id,
                        TransactionDate = transaction.Created.UtcDateTime,
                        ImgUrl = transaction.Merchant.Logo,
                        PotId = 0,
                        MerchantName = transaction.Merchant.Name,
                        Processed = false,
                        TransactionAmount = positiveTransactionAmount / 100
                    });
                }

                context.Transactions.AddRange(transactionListToDb);
                context.SaveChanges();
            }

            Log.Information($"[Monzo Transactions Update] Transactions added into the database");
        }

        public static async Task CreateFeedItem(string moneyType, string moneyLeft)
        {
            var client = new RestClient("https://api.monzo.com/");
            var request = new RestRequest($"feed", Method.Post);
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(AppConfig.AccessToken, "Bearer");
            request.AddParameter("client_id", AppConfig.ClientId);
            request.AddParameter("type", "basic");
            request.AddParameter("account_id", AppConfig.AccountId);
            request.AddParameter("params[title]", $"{moneyType} Balance Updated. Left - {moneyLeft}");
            request.AddParameter("params[body]", $"Money left - {moneyLeft}");
            request.AddParameter("params[image_url]", "https://i.imgur.com/iHpONMX.png");

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Log.Information("[Monzo Feed] Monzo Feed Updated");
            }
            else
            {
                Log.Error($"[Monzo Feed] Monzo Feed failed to update - {response.StatusCode} / {response.StatusDescription} / {response.StatusDescription}");
            }
        }
    }
}
