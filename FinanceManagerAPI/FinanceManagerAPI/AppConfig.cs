using FinanceManagerAPI.Database.Context;
using Serilog;

namespace FinanceManagerAPI
{
    public class AppConfig
    {
        public static string ClientId { get; private set; }
        public static string AccountId { get; private set; }
        public static string ClientSecret { get; private set; }
        public static string RefreshToken { get; private set; }
        public static string AccessToken { get; private set; }

        public static void LoadConfigVariables()
        {
            using(var context = new DatabaseContext())
            {
                var config = context.Config.First();
                
                ClientId = config.ClientId;
                AccountId = config.AccountId;
                ClientSecret = config.ClientSecret;
                RefreshToken = config.RefreshToken;
                AccessToken = config.AccessToken;

                Log.Information("Config loaded");
            }
        }

        public static void UpdateAccessAndRefreshToken(string accessToken, string refreshToken)
        {
            using(var context = new DatabaseContext())
            {
                var config = context.Config.First();
                config.RefreshToken = refreshToken;
                config.AccessToken = accessToken;
                context.SaveChanges();
            }
        }
    }
}
