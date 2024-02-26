using FinanceManagerAPI.Database.Context;
using Serilog;

namespace FinanceManagerAPI
{
    public class AppConfig
    {
        public static string MonzoClientId { get; private set; } = "";
        public static string MonzoAccountId { get; private set; } = "";
        public static string MonzoClientSecret { get; private set; } = "";
        public static string MonzoRefreshToken { get; private set; } = "";
        public static string MonzoAccessToken { get; private set; } = "";
        public static string HFUsername { get; private set; } = "";
        public static string HFPassword { get; private set; } = "";
        public static string ToEmailAddress { get; private set; } = "";
        public static string ToEmailAddressName { get; private set; } = "";
        public static string FromEmailAddress { get; private set; } = "";
        public static string FromEmailAddressName { get; private set; } = "";
        public static string UnprocessedTransactionsTemplateId { get; private set; } = "";
        public static string UpdatedTransactionTemplateId { get; private set; } = "";
        public static long ChatId { get; private set; }
        public static string MMSApiKey { get; private set; } = "";

        public static void LoadConfig()
        {
            using (var context = new DatabaseContext())
            {
                var config = context.Config.First();

                MonzoClientId = config.MonzoClientId;
                MonzoAccountId = config.MonzoAccountId;
                MonzoClientSecret = config.MonzoClientSecret;
                MonzoRefreshToken = config.MonzoRefreshToken;
                MonzoAccessToken = config.MonzoAccessToken;
                HFUsername = config.HFUsername;
                HFPassword = config.HFPassword;
                ToEmailAddress = config.ToEmailAddress;
                ToEmailAddressName = config.ToEmailAddressName;
                FromEmailAddress = config.FromEmailAddress;
                FromEmailAddressName = config.FromEmailAddressName;
                UnprocessedTransactionsTemplateId = config.UnprocessedTransactionsTemplateId;
                UpdatedTransactionTemplateId = config.UpdatedTransactionTemplateId;
                ChatId = config.ChatId;
                MMSApiKey = config.MMSApiKey;

                Log.Information("Config loaded");
            }
        }

        public static async Task UpdateAccessAndRefreshToken(string accessToken, string refreshToken)
        {
            using (var context = new DatabaseContext())
            {
                var config = context.Config.First();
                config.MonzoRefreshToken = refreshToken;
                config.MonzoAccessToken = accessToken;

                MonzoRefreshToken = refreshToken;
                MonzoAccessToken = accessToken;

                await context.SaveChangesAsync();
            }
        }
    }
}
