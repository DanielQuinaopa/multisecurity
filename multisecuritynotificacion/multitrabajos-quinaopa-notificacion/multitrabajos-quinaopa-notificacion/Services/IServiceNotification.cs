using multitrabajos_quinaopa_notificacion.Models;

namespace multitrabajos_quinaopa_notificacion.Services
{
    public interface IServiceNotification
    {
        Task<IEnumerable<Notificacion>> GetAll();
        Task<Notificacion> GetById(int id);
        Task<bool> Create(Notificacion notificacion);
    }
}
