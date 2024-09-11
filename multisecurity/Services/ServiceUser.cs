using Microsoft.EntityFrameworkCore;
using multisecurity.Models;
using multisecurity.Repositories;

namespace multisecurity.Services
{
    public class ServiceUser : IServiceUser
    {
        private readonly Contexto _contexto;
        private readonly IConfiguration _configuration;
        public ServiceUser(Contexto contexto, IConfiguration configuration)
        {
            _contexto = contexto;
            _configuration = configuration;
        }
        public async Task<IEnumerable<User>> getAll()
        {
            var result = await _contexto.Usuario.Where(x => x.Estado.Equals("A")).ToListAsync();
            return result;
        }

        public async Task<User> getUserbyEmail(string email)
        {
            var result = await _contexto.Usuario.Where(x => x.Estado.Equals("A") && x.Email.Equals(email)).FirstOrDefaultAsync();
            return result;
        }

        public async Task<User> getUserbyId(int id)
        {
            var result = await _contexto.Usuario.Where(x => x.Estado.Equals("A") && x.Id.Equals(id)).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> save(User user)
        {
            try
            {
                var userExist = await getUserbyEmail(user.Email);
                if (userExist != null)
                {
                    return false;
                }
                user.Add = DateTime.Now;
                user.Estado = "A";
                _contexto.Add(user);
                return await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public async Task<bool> update(User user)
        {
            try
            {
                var userExist = await getUserbyId(user.Id);
                if (userExist != null)
                {
                    _contexto.Entry(user).State = EntityState.Modified;
                    return await _contexto.SaveChangesAsync() > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        public async Task<bool> delete(int id)
        {
            try
            {
                var userExist = await getUserbyId(id);
                if (userExist != null)
                {
                    userExist.Estado = "I";
                    _contexto.Entry(userExist).State = EntityState.Modified;
                    return await _contexto.SaveChangesAsync() > 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
