using multitrabajos_quinaopa_notificacion.Repository;

namespace multitrabajos_quinaopa_notificacion.Data
{
    public class DbInitializer
    {
        public static void Initialize(ContextDatabase context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
