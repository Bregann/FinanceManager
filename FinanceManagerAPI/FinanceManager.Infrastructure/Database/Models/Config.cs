using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceManagerAPI.Database.Models
{
    public class Config
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigName { get; set; }
        public required string MonzoClientId { get; set; }
        public required string MonzoAccountId { get; set; }
        public required string MonzoClientSecret { get; set; }
        public required string MonzoRefreshToken { get; set; }
        public required string MonzoAccessToken { get; set; }
        public required string HFUsername { get; set; }
        public required string HFPassword { get; set; }
    }
}
