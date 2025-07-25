﻿using BreganUtils;
using FinanceManager.Domain.Dtos.Controllers.Transactions.Requests;
using FinanceManager.Domain.Dtos.Controllers.Transactions.Responses;
using FinanceManagerAPI;
using FinanceManagerAPI.Database.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FinanceManager.Domain.Data.ControllerData
{
    public class TransactionsData
    {
        public static async Task<GetUnprocessedTransactionsDto[]> GetUnprocessedTransactionsFromDatabase()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Transactions.Where(x => x.Processed == false).OrderByDescending(x => x.TransactionDate).Select(x => new GetUnprocessedTransactionsDto
                {
                    Id = x.Id,
                    TransactionAmount = $"£{Math.Round(x.TransactionAmount / 100m, 2)}",
                    IconUrl = x.ImgUrl,
                    MerchantName = x.MerchantName,
                    TransactionDate = DateTimeHelper.HumanizeDateTime(x.TransactionDate)
                }).ToArrayAsync();
            }
        }

        public static async Task<GetProcessedTransactionsForMonthDto[]> GetProcessedTransactionsForMonth()
        {
            using (var context = new DatabaseContext())
            {
                var startMonthDate = context.HistoricData.OrderByDescending(x => x.Id).First().MonthStart.Date;

                return await context.Transactions.Where(x => x.Processed && x.TransactionDate.Date >= startMonthDate).Select(x => new GetProcessedTransactionsForMonthDto
                {
                    Id = x.Id,
                    TransactionAmount = $"£{Math.Round(x.TransactionAmount / 100m, 2)}",
                    IconUrl = x.ImgUrl,
                    MerchantName = x.MerchantName,
                    PotId = x.Pot != null ? x.Pot.Id.ToString() : null,
                    TransactionDate = DateTimeHelper.HumanizeDateTime(x.TransactionDate)
                }).ToArrayAsync();
            }
        }

        public static async Task<bool> UpdateTransaction(UpdateTransactionRequest request)
        {
            using (var context = new DatabaseContext())
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == request.TransactionId);
                var pot = await context.Pots.FirstOrDefaultAsync(x => x.Id == request.PotId);

                if (transaction == null || pot == null)
                {
                    Log.Information("[Transactions] Invalid request - transaction or pot was null");
                    return false;
                }

                //Check if there is a pot already assigned to the transaction
                if (transaction.PotId != null)
                {
                    //Add back the money into the pot
                    transaction.Pot!.PotAmountLeft += transaction.TransactionAmount;
                    transaction.Pot!.PotAmountSpent -= transaction.TransactionAmount;
                    await context.SaveChangesAsync();

                    Log.Information("[Transactions] Old pot updated");
                }

                transaction.Pot = pot;
                transaction.Processed = true;
                pot.PotAmountLeft -= transaction.TransactionAmount;
                pot.PotAmountSpent += transaction.TransactionAmount;

                var culture = CultureInfo.GetCultureInfo("en-GB");

                //Send communications
                MessageHelper.SendTextMessage(AppConfig.MMSApiKey, AppConfig.ChatId, $"Transaction @ {transaction.MerchantName} \n Set to pot {pot.PotName} \n Transaction amount: £{Math.Round(transaction.TransactionAmount / 100m, 2).ToString("N", culture)} \n Amount left in pot: £{Math.Round(pot.PotAmountLeft / 100m, 2).ToString("N", culture)}");

                var emailContent = new
                {
                    merchantName = transaction.MerchantName,
                    transactionAmount = $"£{Math.Round(transaction.TransactionAmount / 100m, 2).ToString("N", culture)}",
                    potName = pot.PotName,
                    potAmountLeft = $"£{Math.Round(pot.PotAmountLeft / 100m, 2).ToString("N", culture)}"
                };

                MessageHelper.SendEmail(AppConfig.MMSApiKey, AppConfig.ToEmailAddress, AppConfig.ToEmailAddressName, AppConfig.FromEmailAddress, AppConfig.FromEmailAddressName, emailContent, AppConfig.UpdatedTransactionTemplateId);

                await context.SaveChangesAsync();
                Log.Information("[Transactions] Pot and transaction updated");
                return true;
            }
        }

        public static async Task<bool> DeleteTransaction(string transactionId)
        {
            using (var context = new DatabaseContext())
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(x => x.Id == transactionId);

                if (transaction == null)
                {
                    return false;
                }

                if (transaction.Pot != null)
                {
                    //Add back the money into the pot
                    transaction.Pot!.PotAmountLeft += transaction.TransactionAmount;
                    transaction.Pot!.PotAmountSpent -= transaction.TransactionAmount;
                    transaction.Pot = null;
                    transaction.Processed = true;
                    await context.SaveChangesAsync();

                    Log.Information("[Transactions] Transaction with pot marked as processed");

                    return true;
                }

                transaction.Processed = true;
                await context.SaveChangesAsync();
                Log.Information("[Transactions] Transaction without pot marked as processed");

                return true;
            }
        }
    }
}
