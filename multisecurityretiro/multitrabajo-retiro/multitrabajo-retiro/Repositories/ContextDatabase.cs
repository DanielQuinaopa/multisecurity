using Microsoft.EntityFrameworkCore;
using multitrabajo_retiro.Models;

namespace multitrabajo_retiro.Repositories
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        {
        }

        public DbSet<Transaction> Transaction { get; set; }
    }
}
