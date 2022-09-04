using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinanceManagerAPI.Database.Models
{
    public class Config
    {
        [Key]
        [Column("configName")]
        public string ConfigName { get; set; }

        [Column("clientID")]
        public string ClientId { get; set; }

        [Column("accountID")]
        public string AccountId { get; set; }

        [Column("clientSecret")]
        public string ClientSecret { get; set; }

        [Column("refreshToken")]
        public string RefreshToken { get; set; }

        [Column("accessToken")]
        public string AccessToken { get; set; }

    }
}
