using Microsoft.EntityFrameworkCore;

namespace FinanceManager.Domain.Database.Context
{
    public class SqliteContext : AppDbContext
    {
        public SqliteContext(DbContextOptions<SqliteContext> options) : base(options) { }
    }
}
