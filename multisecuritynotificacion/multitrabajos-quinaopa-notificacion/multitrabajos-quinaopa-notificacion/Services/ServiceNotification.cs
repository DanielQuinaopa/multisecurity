using multitrabajos_quinaopa_notificacion.Models;
using multitrabajos_quinaopa_notificacion.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace multitrabajos_quinaopa_notificacion.Services
{
    public class ServiceNotification : IServiceNotification
    {
        private readonly ContextDatabase _contextDatabase;

        public ServiceNotification(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public async Task<IEnumerable<Notificacion>> GetAll()
        {
            try
            {
                var result = await _contextDatabase.Notificaciones.ToListAsync();
                return result;
            }
            catch
            {

                return null!;
            }

        }

        public async Task<Notificacion> GetById(int id)
        {
            var res = await _contextDatabase.Notificaciones.Where(x => x.Id.Equals(id))
                                  .AsNoTracking().FirstOrDefaultAsync();
            if (res != null)
            {
                return res;
            }
            else
            {
                return null!;
            }
        }

        public async Task<bool> Create(Notificacion notificacion)
        {
            _contextDatabase.Notificaciones.Add(notificacion);
            return await _contextDatabase.SaveChangesAsync() > 0;
        }
        //public async Task<bool> Deposit(Notificacion account)
        //{
        //    _contextDatabase.Entry(account).State = EntityState.Modified;
        //    return await _contextDatabase.SaveChangesAsync() > 0;
        //}
        //public async Task<bool> Withdrawal(Notificacion account)
        //{
        //    _contextDatabase.Entry(account).State = EntityState.Modified;
        //    return await _contextDatabase.SaveChangesAsync() > 0;
        //}
    }
}
