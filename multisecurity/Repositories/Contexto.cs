using Microsoft.EntityFrameworkCore;
using multisecurity.Models;

namespace multisecurity.Repositories
{
    public class Contexto : DbContext
    {
        public Contexto()//Conexiones
        { 

        }
        public Contexto(DbContextOptions<Contexto> options):base(options) 
        {

        }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<User> Usuario { get; set; }
    }
}
