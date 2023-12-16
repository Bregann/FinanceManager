using FinanceManager.Domain.Dtos.Controllers.Pots.Responses;
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
            using(var context = new DatabaseContext())
            {
                return await context.Pots.Where(x => x.Deleted == false).OrderBy(x => x.Id).Select(x => new GetPotsStatsDto
                {
                    PotId = x.Id,
                    PotName = x.PotName,
                    IsSavingsPot = x.IsSavingsPot,
                    AmountAllocated = $"£{Math.Round(x.PotAmount / 100m, 2)}",
                    AmountLeft = $"£{Math.Round(x.PotAmountLeft / 100m, 2)}",
                    AmountSpent = $"£{Math.Round(x.PotAmountSpent / 100m, 2)}"
                }).ToArrayAsync();
            }
        }
    }
}
