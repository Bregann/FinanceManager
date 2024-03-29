﻿using FinanceManager.Domain.Dtos.Controllers.Pots.Requests;
using FinanceManager.Domain.Dtos.Controllers.Pots.Responses;
using FinanceManager.Domain.Dtos.Controllers.Shared.Response;
using FinanceManager.Infrastructure.Database.Models;
using FinanceManagerAPI.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Domain.Data.ControllerData
{
    public class PotsData
    {
        public static async Task<PotsDropdownValuesDto[]> GetPotDropdownValues()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Pots.Where(x => x.Deleted == false && x.IsSavingsPot == false).Select(x => new PotsDropdownValuesDto
                {
                    Value = x.Id.ToString(),
                    Label = x.PotName
                }).ToArrayAsync();
            }
        }

        public static async Task<GetPotsStatsDto[]> GetPotsStats()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Pots.Where(x => x.Deleted == false).OrderBy(x => x.Id).Select(x => new GetPotsStatsDto
                {
                    PotId = x.Id,
                    PotName = x.PotName,
                    IsSavingsPot = x.IsSavingsPot,
                    AmountAllocated = $"£{Math.Round(x.PotAmount / 100m, 2)}",
                    AmountLeft = Math.Round(x.PotAmountLeft / 100m, 2),
                    AmountSpent = $"£{Math.Round(x.PotAmountSpent / 100m, 2)}"
                }).ToArrayAsync();
            }
        }

        public static async Task<GetPotListDto[]> GetPotList()
        {
            using (var context = new DatabaseContext())
            {
                return await context.Pots.Where(x => x.Deleted == false && x.Id > 1).OrderBy(x => x.Id).Select(x => new GetPotListDto
                {
                    PotId = x.Id,
                    PotName = x.PotName,
                    PotAmount = Math.Round(x.PotAmount / 100m, 2),
                    IsSavingsPot = x.IsSavingsPot
                }).ToArrayAsync();
            }
        }

        public static async Task<AddNewPotDto> AddNewPot(AddNewPotRequest request)
        {
            using (var context = new DatabaseContext())
            {
                if (context.Pots.Where(x => x.Deleted == false).Any(x => x.PotName.ToLower() == request.PotName.ToLower().Trim()))
                {
                    return new AddNewPotDto
                    {
                        Success = false,
                        Reason = "Pot with that name already exists"
                    };
                }

                var amount = (long)(request.Amount * 100);

                var newPot = new Pots
                {
                    PotName = request.PotName.Trim(),
                    IsSavingsPot = request.IsSavingsPot,
                    PotAmount = amount,
                    PotAmountLeft = amount,
                    PotAmountSpent = 0,
                    Deleted = false
                };

                await context.Pots.AddAsync(newPot);
                await context.SaveChangesAsync();

                return new AddNewPotDto
                {
                    Success = true,
                    Reason = "Pot added succesfully",
                    PotId = newPot.Id
                };
            }
        }

        public static async Task<BoolReasonDto> UpdatePot(UpdatePotRequest request)
        {
            using (var context = new DatabaseContext())
            {
                if(request.PotId == 1)
                {
                    return new BoolReasonDto
                    {
                        Success = false,
                        Reason = "Pot cannot be changed"
                    };
                }

                var pot = await context.Pots.FirstOrDefaultAsync(x => x.Id == request.PotId);
                if (pot == null)
                {
                    return new BoolReasonDto
                    {
                        Success = false,
                        Reason = "Pot with that id does not exist"
                    };
                }

                //Check if the name is being updated
                if (pot.PotName.ToLower() != request.PotName.Trim().ToLower() && context.Pots.Any(x => x.PotName.ToLower() == request.PotName.Trim().ToLower()))
                {
                    return new BoolReasonDto
                    {
                        Success = false,
                        Reason = "Pot with that name already exists"
                    };
                }

                pot.PotName = request.PotName.Trim();
                pot.PotAmount = (long)(request.PotAmount * 100);

                await context.SaveChangesAsync();

                return new BoolReasonDto
                {
                    Success = true,
                    Reason = "Pot was updated succesfully"
                };
            }
        }

        public static async Task<bool> DeletePot(int potId)
        {
            using (var context = new DatabaseContext())
            {
                if(potId == 1)
                {
                    return false;
                }

                var pot = await context.Pots.FirstOrDefaultAsync(x => x.Id == potId);

                if (pot == null)
                {
                    return false;
                }

                pot.Deleted = true;
                await context.SaveChangesAsync();

                return true;
            }
        }

        public static async Task<UpdateSavingsPotDto> UpdateSavingsPot(UpdateSavingsPotRequest request)
        {
            using (var context = new DatabaseContext())
            {
                var pot = await context.Pots.FirstOrDefaultAsync(x => x.Id == request.PotId);

                if (pot == null)
                {
                    return new UpdateSavingsPotDto
                    {
                        Success = false,
                        Reason = "Pot does not exist"
                    };
                }

                var savingsInPence = (long)(request.SavingsAmount * 100);

                pot.PotAmountLeft = savingsInPence;
                await context.SaveChangesAsync();

                return new UpdateSavingsPotDto
                {
                    Success = true,
                    Reason = "",
                    Amount = Math.Round(pot.PotAmountLeft / 100m, 2)
                };
            }
        }
    }
}
