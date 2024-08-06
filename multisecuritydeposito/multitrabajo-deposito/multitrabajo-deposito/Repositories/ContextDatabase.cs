using Microsoft.EntityFrameworkCore;
using multitrabajo_deposito.Models;

namespace multitrabajo_deposito.Repositories
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        {
        }

        public DbSet<Transaction> Transaction { get; set; }
    }
}
