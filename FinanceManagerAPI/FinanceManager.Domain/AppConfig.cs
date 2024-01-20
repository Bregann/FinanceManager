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
