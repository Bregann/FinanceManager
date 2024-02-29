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
        public required string ToEmailAddress { get; set; }
        public required string ToEmailAddressName { get; set; }
        public required string FromEmailAddress { get; set; }
        public required string FromEmailAddressName { get; set; }
        public required string UpdatedTransactionTemplateId { get; set; }
        public required string DailyStatsTemplateId { get; set; }
        public required long ChatId { get; set; }
        public required string MMSApiKey { get; set; }
    }
}
