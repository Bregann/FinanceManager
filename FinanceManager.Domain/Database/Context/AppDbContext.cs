﻿using FinanceManager.Domain.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Domain.Database.Context
{
    public partial class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<EnvironmentalSetting> EnvironmentalSettings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; } = null!;

        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<SpendingPot> Pots { get; set; }
        public DbSet<AutomaticTransaction> AutomaticTransactions { get; set; }
        public DbSet<HistoricMonthlyData> HistoricData { get; set; }
    }
}
