using FinanceManagerAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagerAPI.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Pots> Pots { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Config> Config { get; set; }
        public DbSet<AutomaticTransactions> AutomaticTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //By default there's an unallocated pot for extra money that isn't allocated
            modelBuilder.Entity<Pots>().HasData(new Pots { AmountAllocated = 0, AmountLeftThisMonth = 0, PotId = 1, PotName = "Unallocated" });

            //Config needs to be set manually
            modelBuilder.Entity<Config>().HasData(new Config { ConfigName = "Main", AccessToken = "", ClientId = "", AccountId = "", ClientSecret = "", RefreshToken = ""});

#if DEBUG
            //Test transaction data
            modelBuilder.Entity<Transactions>().HasData(new Transactions { Id = "abc123", ImgUrl = "https://mondo-logo-cache.appspot.com/twitter/@Morrisons/?size=large','2022-08-31", MerchantName = "Morrisons", PotId = 0, Processed = false, TransactionAmount = 5.44M, TransactionDate = DateTime.Today.AddDays(-2) },
                new Transactions { Id = "abc1234", ImgUrl = "https://mondo-logo-cache.appspot.com/twitter/@Morrisons/?size=large','2022-08-31", MerchantName = "Greggs", PotId = 0, Processed = false, TransactionAmount = 2.44M, TransactionDate = DateTime.Today.AddDays(-3) },
                new Transactions { Id = "abc1235", ImgUrl = "https://mondo-logo-cache.appspot.com/twitter/@Morrisons/?size=large','2022-08-31", MerchantName = "Sainsburys", PotId = 0, Processed = false, TransactionAmount = 44.44M, TransactionDate = DateTime.Today.AddDays(-4) });
#endif
        }

#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=financedev;Username=finance;Password=finance");
#else
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=finance;Username=finance;Password=finance");
#endif
    }
}
