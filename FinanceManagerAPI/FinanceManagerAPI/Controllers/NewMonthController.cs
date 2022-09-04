using FinanceManagerAPI.Database.Context;
using FinanceManagerAPI.Objects.NewMonth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewMonthController : ControllerBase
    {
        [HttpPost("GetAddMonthBreakdownAndTotal")]
        public GetAddMonthBreakdownDto GetAddMonthBreakdownAndTotal([FromBody] GetAddMonthBreakdownDto dto)
        {
            //Check if it's to 2 decimal places
            if (decimal.Round(dto.Income, 2) != dto.Income)
            {
                return new GetAddMonthBreakdownDto
                {
                    Success = false,
                    Error = "Invalid number"
                };
            }

            decimal total = 0;
            var potsForDto = new List<Pot>();

            using (var context = new DatabaseContext())
            {
                foreach(var pots in context.Pots)
                {
                    //Skip pot 1 as its default and has nothing allocated
                    if (pots.PotId == 1)
                    {
                        continue;
                    }

                    potsForDto.Add(new Pot
                    {
                        PotName = pots.PotName,
                        AmountAllocated = $"£{pots.AmountAllocated:N2}"
                    });

                    total += pots.AmountAllocated;
                }
            }

            return new GetAddMonthBreakdownDto
            {
                Success = true,
                Total = $"£{total:N2}",
                SpareMoney = $"£{dto.Income - total:N2}",
                Pots = potsForDto
            };
        }

        [HttpPost("CreateNewMonth")]
        public CreateNewMonthDto CreateNewMonth([FromBody] CreateNewMonthDto dto)
        {
            //Check if it's to 2 decimal places
            if (decimal.Round(dto.Income, 2) != dto.Income)
            {
                return new CreateNewMonthDto
                {
                    Success = false,
                    Error = "Invalid number"
                };
            }

            decimal total = 0;

            using(var context = new DatabaseContext())
            {
                //Set all the values
                foreach (var pots in context.Pots)
                {
                    //id 1 is default, don't change it
                    if(pots.PotId == 1)
                    {
                        continue;
                    }

                    pots.AmountLeftThisMonth = pots.AmountAllocated;
                    total += pots.AmountAllocated;
                }

                //calculate the money left and set that to the spare money pot
                context.Pots.First(x => x.PotId == 1).AmountLeftThisMonth = dto.Income - total;
                context.Pots.First(x => x.PotId == 1).AmountAllocated = dto.Income - total;

                context.SaveChanges();
            }

            return new CreateNewMonthDto 
            { 
                Success = true 
            };
        }
    }
}
