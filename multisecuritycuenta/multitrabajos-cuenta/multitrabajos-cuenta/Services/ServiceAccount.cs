using Microsoft.EntityFrameworkCore;
using multitrabajos_cuenta.Models;
using multitrabajos_cuenta.Repository;

namespace multitrabajos_cuenta.Services
{
    public class ServiceAccount : IServiceAccount
    {
        //Se puede utilizar solo en esta clase 
        private readonly ContextDatabase _contextDatabase;
        public ServiceAccount(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            try
            {
                var result = await _contextDatabase.Account.Include(x => x.Customer).ToListAsync();
                return result;
            }
            catch
            {

                return null!;
            }
        }

        public async Task<Account> GetbyId(int id)
        {
            var res = await _contextDatabase.Account.Where(x => x.IdAccount.Equals(id))
                                              .Include(x => x.Customer).AsNoTracking().FirstOrDefaultAsync();
            if (res != null)
            {
                return res;
            }
            else
            {
                return null!;
            }
        }
        public async Task<bool> Deposit(Account account)
        {
            _contextDatabase.Entry(account).State = EntityState.Modified;
            return await _contextDatabase.SaveChangesAsync() > 0;
        }
        public async Task<bool> Withdrawal(Account account)
        {
            _contextDatabase.Entry(account).State = EntityState.Modified;
            return await _contextDatabase.SaveChangesAsync() > 0;
        }
        public async Task<bool> NewAccount(Customer customer)
        {
            try
            {
                await _contextDatabase.Customer.AddAsync(customer);
                await _contextDatabase.Account.AddAsync(new Account
                {
                    IdAccount = customer.IdCustomer,
                    TotalAmount = 0,
                    IdCustomer = customer.IdCustomer
                });
                // Guardar los cambios en la base de datos
                await _contextDatabase.SaveChangesAsync();
                return true;

            }
            catch (System.Exception ex)
            {

                return false;
            }
        }
    }
}
