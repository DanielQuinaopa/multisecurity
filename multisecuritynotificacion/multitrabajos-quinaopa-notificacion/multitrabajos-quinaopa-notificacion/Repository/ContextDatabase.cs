using Microsoft.EntityFrameworkCore;
using multitrabajos_quinaopa_notificacion.Models;

namespace multitrabajos_quinaopa_notificacion.Repository
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        {
        }

        public DbSet<Notificacion> Notificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.ToTable("notificaciones");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdCuenta).HasColumnName("idCuenta");
                entity.Property(e => e.Tipo).HasColumnName("tipo").IsRequired();
                entity.Property(e => e.Valor).HasColumnName("valor").IsRequired();            
            });
        }
    }
}


