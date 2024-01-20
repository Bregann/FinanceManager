using FinanceManager.Infrastructure.Database.Models;
using FinanceManagerAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManagerAPI.Database.Context
{
    public class DatabaseContext : DbContext
    {
        private static readonly string _connectionString = Environment.GetEnvironmentVariable("FMApiConnString")!;

        public DbSet<Config> Config { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Pots> Pots { get; set; }
        public DbSet<AutomaticTransactions> AutomaticTransactions { get; set; }
        public DbSet<HistoricData> HistoricData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pots>().HasData(new Pots
            {
                Id = 1,
                IsSavingsPot = false,
                PotAmount = 0,
                PotAmountLeft = 0,
                PotAmountSpent = 0,
                PotName = "Spare Money",
                Deleted = false
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .UseLazyLoadingProxies()
            .UseNpgsql(_connectionString);
    }
}
