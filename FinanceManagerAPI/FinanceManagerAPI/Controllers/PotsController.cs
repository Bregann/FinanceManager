using FinanceManagerAPI.Database.Context;
using FinanceManagerAPI.Database.Models;
using FinanceManagerAPI.Dtos.Pots;
using FinanceManagerAPI.Objects.Pots;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FinanceManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PotsController : ControllerBase
    {
        [HttpGet("GetPotsHeader")]
        public List<PotsHeaderDto> GetPotsHeader()
        {
            using(var context = new DatabaseContext())
            {
                var potList = new List<PotsHeaderDto>();

                foreach (var pot in context.Pots)
                {
                    potList.Add(new PotsHeaderDto
                    {
                        PotName = pot.PotName,
                        AmountAllocated = $"£{pot.AmountAllocated:N2}",
                        AmountLeftForMonth = $"£{pot.AmountLeftThisMonth:N2}"
                    });
                }

                return potList;
            }
        }

        [HttpGet("GetPotValuesDropdown")]
        public List<PotsDropdownDto> GetPotValuesDropdown()
        {
            using(var context = new DatabaseContext())
            {
                return (context.Pots.OrderBy(x => x.PotId).Select(pot => new PotsDropdownDto
                {
                    Value = pot.PotId,
                    Label = pot.PotName
                })).ToList();
            }
        }

        [HttpPost("DoesPotExist")]
        public bool DoesPotExist([FromBody] string potName)
        {
            using(var context = new DatabaseContext())
            {
                var pot = context.Pots.FirstOrDefault(x => x.PotName.ToLower() == potName.ToLower());

                if (pot == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [HttpPost("AddNewPot")]
        public NewPotDto AddNewPot([FromBody] NewPotDto dto)
        {
            //Check if it's to 2 decimal places
            if (decimal.Round(dto.PotAmount, 2) != dto.PotAmount)
            {
                return new NewPotDto
                {
                    Success = false,
                    Error = "Invalid number"
                };
            }

            using (var context = new DatabaseContext())
            {
                //Check if the pot exists
                var pot = context.Pots.FirstOrDefault(x => x.PotName.ToLower() == dto.PotName.ToLower());

                if (pot != null)
                {
                    return new NewPotDto
                    {
                        Success = false,
                        Error = "Pot already exists"
                    };
                }

                context.Pots.Add(new Pots
                {
                    PotName = dto.PotName,
                    AmountAllocated = dto.PotAmount,
                    AmountLeftThisMonth = dto.PotAmount
                });

                context.SaveChanges();

                return new NewPotDto
                {
                    Success = true
                };
            }
        }

        [HttpPost("GetSpecificPotData")]
        public SpecificPotDataDto GetSpecificPotData([FromBody] SpecificPotDataDto dto)
        {
            using(var context = new DatabaseContext())
            {
                //Check if the pot exists
                var pot = context.Pots.FirstOrDefault(x => x.PotId == dto.PotId);

                if (pot == null)
                {
                    return new SpecificPotDataDto
                    {
                        Success = false,
                        Error = "Pot does not exist"
                    };
                }

                return new SpecificPotDataDto
                {
                    Success = true,
                    PotAmount = pot.AmountAllocated,
                    PotName = pot.PotName
                };
            }
        }

        [HttpPost("EditPot")]
        public EditPotDto EditPot([FromBody] EditPotDto dto)
        {
            //if 1 then don't allow it to be deleted 
            if (dto.PotId == 1)
            {
                return new EditPotDto
                {
                    Success = false,
                    Error = "Pot cannot be deleted"
                };
            }

            using (var context = new DatabaseContext())
            {
                var potToUpdate = context.Pots.First(x => x.PotId == dto.PotId);

                //Check if the name has changed, if so then make sure that pot name doesn't already exist
                if (dto.NameChanged)
                {
                    var pot = context.Pots.FirstOrDefault(x => x.PotName.ToLower() == dto.PotName.ToLower());

                    if (pot != null)
                    {
                        return new EditPotDto
                        {
                            Success = false,
                            Error = "Pot already exists"
                        };
                    }
                }

                //Check if it's a negative amount
                if (dto.PotAmount <= 0)
                {
                    return new EditPotDto
                    {
                        Success = false,
                        Error = "Pot amount cannot be 0"
                    };
                }

                //Set the values and update the pot
                potToUpdate.PotName = dto.PotName;
                potToUpdate.AmountAllocated = dto.PotAmount;

                context.Update(potToUpdate);
                context.SaveChanges();

                return new EditPotDto
                {
                    Success = true
                };
            }
        }

        [HttpDelete("DeletePot")]
        public IActionResult DeletePot(DeletePotDto dto)
        {
            //if 1 then don't allow it to be deleted 
            if (dto.PotId == 1)
            {
                return NotFound();
            }

            using (var context = new DatabaseContext())
            {
                var pot = context.Pots.Where(x => x.PotId == dto.PotId).FirstOrDefault();

                if (pot == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Pots.Remove(pot);
                    context.SaveChanges();
                    return Ok();
                }
            }
        }
    }
}
