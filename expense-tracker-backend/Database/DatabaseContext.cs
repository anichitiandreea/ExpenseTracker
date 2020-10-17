using expense_tracker_backend.Database.Configuration;
using expense_tracker_backend.Domain;
using expense_tracker_backend.Domain.Domains;
using Microsoft.EntityFrameworkCore;

namespace expense_tracker_backend.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new CurrencyConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql("Server=localhost;Port=5432;Database=ExpenseTracker;User Id=postgres;Password=parola;");
        }
    }
}
