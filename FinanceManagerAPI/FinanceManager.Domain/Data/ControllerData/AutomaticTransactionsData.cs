using FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Request;
using FinanceManager.Domain.Dtos.Controllers.AutomaticTransactions.Responses;
using FinanceManager.Domain.Dtos.Controllers.Shared.Response;
using FinanceManager.Infrastructure.Database.Models;
using FinanceManagerAPI.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Domain.Data.ControllerData
{
    public class AutomaticTransactionsData
    {
        public static async Task<AutomaticTransactionDto[]> GetAutomaticTransactions()
        {
            using (var context = new DatabaseContext())
            {
                return await context.AutomaticTransactions.Select(x => new AutomaticTransactionDto
                {
                    Id = x.Id,
                    MerchantName = x.MerchantName,
                    PotId = x.Pot.Id.ToString(),
                }).ToArrayAsync();
            }
        }

        public static async Task<AddAutomaticTransactionDto> AddAutomaticTransaction(AddAutomaticTransactionReqest request)
        {
            using (var context = new DatabaseContext())
            {
                if (context.AutomaticTransactions.Any(x => x.MerchantName.ToLower() == request.MerchantName.ToLower().Trim()))
                {
                    return new AddAutomaticTransactionDto
                    {
                        Success = false,
                        Reason = "Automatic transaction for merchant already exists"
                    };
                }

                if (!context.Pots.Any(x => x.Id == request.PotId))
                {
                    return new AddAutomaticTransactionDto
                    {
                        Success = false,
                        Reason = "Pot does not exist with that id"
                    };
                }

                var newTransaction = new AutomaticTransactions
                {
                    MerchantName = request.MerchantName.Trim(),
                    PotId = request.PotId
                };

                await context.AutomaticTransactions.AddAsync(newTransaction);

                await context.SaveChangesAsync();

                return new AddAutomaticTransactionDto
                {
                    Success = true,
                    Reason = "Pot added succesfully",
                    AutomaticTransactionId = newTransaction.Id
                };
            }
        }

        public static async Task<BoolReasonDto> UpdateAutomaticTransaction(UpdateAutomaticTransactionRequest request)
        {
            using (var context = new DatabaseContext())
            {
                var automaticTransaction = await context.AutomaticTransactions.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (automaticTransaction == null)
                {
                    return new BoolReasonDto
                    {
                        Success = false,
                        Reason = "Automatic transaction does not exist with that id"
                    };
                }

                if (!context.Pots.Any(x => x.Id != request.PotId))
                {
                    return new BoolReasonDto
                    {
                        Success = false,
                        Reason = "Pot not exist with that id"
                    };
                }

                automaticTransaction.PotId = request.PotId;
                automaticTransaction.MerchantName = request.MerchantName.Trim();

                await context.SaveChangesAsync();

                return new BoolReasonDto
                {
                    Success = true,
                    Reason = "Automatic transaction has been updated"
                };
            }
        }

        public static async Task<bool> DeleteAutomaticTransaction(int transactionId)
        {
            using (var context = new DatabaseContext())
            {
                return await context.AutomaticTransactions.Where(x => x.Id == transactionId).ExecuteDeleteAsync() > 0;
            }
        }
    }
}
