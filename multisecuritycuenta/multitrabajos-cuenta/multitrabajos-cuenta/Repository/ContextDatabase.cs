using Microsoft.EntityFrameworkCore;
using multitrabajos_cuenta.Models;

namespace multitrabajos_cuenta.Repository
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        {
        }

        public DbSet<Models.Customer> Customer { get; set; }
        public DbSet<Models.Account> Account { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Account>()
                 .Property(a => a.TotalAmount)
                 .HasColumnType("decimal(18,2)");
        }
    }
}
