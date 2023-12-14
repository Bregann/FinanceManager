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
                return await context.Pots.Where(x => x.Deleted == false).Select(x => new PotsDropdownValuesDto
                {
                    Value = x.Id.ToString(),
                    Label = x.PotName
                }).ToArrayAsync();
            }
        }
    }
}
