using BreganUtils;
using FinanceManager.Domain.Dtos.ThirdParty;
using FinanceManager.Infrastructure.Database.Models;
using FinanceManagerAPI.Database.Context;
using FinanceManagerAPI.Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using Serilog;
using System.Globalization;

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
            request.AddParameter("client_id", AppConfig.MonzoClientId);
            request.AddParameter("client_secret", AppConfig.MonzoClientSecret);
            request.AddParameter("refresh_token", AppConfig.MonzoRefreshToken);

            var response = await client.ExecuteAsync(request);

            if (response.Content == null || response.Content == "" || !response.IsSuccessStatusCode)
            {
                Log.Warning($"[Monzo Refresh] Refresh Error - response {response.StatusCode}");
                //todo: send error msg
                return;
            }

            var transactionsResult = JsonConvert.DeserializeObject<MonzoRefreshResponse>(response.Content);
            await AppConfig.UpdateAccessAndRefreshToken(transactionsResult!.AccessToken, transactionsResult.RefreshToken);

            Log.Information("[Monzo Refresh Job] Monzo Refreshed");
        }

        public static async Task GetTransactionsAndAddToDatabase()
        {
            //Create the request
            var clientOptions = new RestClientOptions("https://api.monzo.com/")
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(AppConfig.MonzoAccessToken, "Bearer")
            };

            var client = new RestClient(clientOptions);
            var request = new RestRequest($"/transactions?expand[]=merchant&account_id={AppConfig.MonzoAccountId}&since={DateTime.Now.AddDays(-7).ToString("yyyy-MM-ddTh:mm:ssZ")}", Method.Get);

            //Get the response and Deserialize
            var response = await client.ExecuteAsync(request);

            if (response.Content == null || response.Content == "")
            {
                Log.Warning("[Monzo Transactions Update] No response found");
                return;
            }

            var transactionsResult = JsonConvert.DeserializeObject<MonzoDataResponse>(response.Content);

            if (transactionsResult!.Transactions == null)
            {
                Log.Information("[Monzo Transactions Update] No transactions found");
                return;
            }

            using (var context = new DatabaseContext())
            {
                //loop through the transactions and insert them to the db
                foreach (var transaction in transactionsResult.Transactions)
                {
                    //Check if it already exists, if it does then dont insert
                    if (await context.Transactions.AnyAsync(x => x.Id == transaction.Id))
                    {
                        continue;
                    }

                    //Convert the amount to a positive
                    var positiveTransactionAmount = Math.Abs(transaction.Amount);

                    if (transaction.Merchant == null)
                    {
                        continue;
                    }

                    await context.Transactions.AddAsync(new Transactions
                    {
                        Id = transaction.Id,
                        TransactionDate = transaction.Created.UtcDateTime,
                        ImgUrl = transaction.Merchant.Logo,
                        MerchantName = transaction.Merchant.Name,
                        Processed = false,
                        TransactionAmount = positiveTransactionAmount
                    });
                }

                await context.SaveChangesAsync();
            }

            Log.Information($"[Monzo Transactions Update] Transactions added into the database");
        }

        public static async Task CreateFeedItem(string moneyType, string moneyLeft)
        {
            var clientOptions = new RestClientOptions("https://api.monzo.com/")
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(AppConfig.MonzoAccessToken, "Bearer")
            };

            var client = new RestClient(clientOptions);
            var request = new RestRequest($"feed", Method.Post);
            request.AddParameter("client_id", AppConfig.MonzoClientId);
            request.AddParameter("type", "basic");
            request.AddParameter("account_id", AppConfig.MonzoAccountId);
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

        public static async Task UpdateAutomaticTransactions()
        {
            using (var context = new DatabaseContext())
            {
                var unprocessedTransactions = await context.Transactions.Where(x => x.Processed == false).ToArrayAsync();

                foreach (var transaction in unprocessedTransactions)
                {
                    //Check if there is an automatic transaction for it
                    var automaticTransaction = await context.AutomaticTransactions.FirstOrDefaultAsync(x => x.MerchantName.ToLower() == transaction.MerchantName.ToLower());

                    if (automaticTransaction == null)
                    {
                        continue;
                    }

                    //Update the transaction data
                    automaticTransaction.Pot.PotAmountLeft -= transaction.TransactionAmount;
                    automaticTransaction.Pot.PotAmountSpent += transaction.TransactionAmount;
                    transaction.Pot = automaticTransaction.Pot;
                    transaction.Processed = true;

                    await context.SaveChangesAsync();

                    var culture = CultureInfo.GetCultureInfo("en-GB");

                    //Send communications
                    MessageHelper.SendTextMessage(AppConfig.MMSApiKey, AppConfig.ChatId, $"Transaction @ {transaction.MerchantName} \n Set to pot {automaticTransaction.Pot.PotName} \n Transaction amount: £{Math.Round(transaction.TransactionAmount / 100m, 2).ToString("N", culture)} \n Amount left in pot: £{Math.Round(automaticTransaction.Pot.PotAmountLeft / 100m, 2).ToString("N", culture)}");

                    var emailContent = new
                    {
                        merchantName = transaction.MerchantName,
                        transactionAmount = $"£{Math.Round(transaction.TransactionAmount / 100m, 2).ToString("N", culture)}",
                        potName = automaticTransaction.Pot.PotName,
                        potAmountLeft = $"£{Math.Round(automaticTransaction.Pot.PotAmountLeft / 100m, 2).ToString("N", culture)}"
                    };

                    MessageHelper.SendEmail(AppConfig.MMSApiKey, AppConfig.ToEmailAddress, AppConfig.ToEmailAddressName, AppConfig.FromEmailAddress, AppConfig.FromEmailAddressName, emailContent, AppConfig.UpdatedTransactionTemplateId);


                    Log.Information($"[Automatic Transactions] £{transaction.TransactionAmount} transaction for {transaction.MerchantName} successfully updated");
                }
            }
        }
    }
}
