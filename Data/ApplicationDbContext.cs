using FinancialTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Account> accounts { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<Budget> budgets { get; set; }
        public DbSet<Reminder> reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Account>().ToTable("accounts");
            modelBuilder.Entity<Transaction>().ToTable("transactions");
            modelBuilder.Entity<Budget>().ToTable("budgets");
            modelBuilder.Entity<Reminder>().ToTable("reminders");

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.account)
                .WithMany(a => a.transactions)
                .HasForeignKey(t => t.accountid);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.user)
                .WithMany(u => u.transactions)
                .HasForeignKey(t => t.userid);
        }
    }
}
