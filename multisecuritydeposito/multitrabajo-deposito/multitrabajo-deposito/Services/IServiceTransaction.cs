using multitrabajo_deposito.Models;

namespace multitrabajo_deposito.Services
{
    public interface IServiceTransaction
    {
        Task<Transaction> Deposit(Transaction transaction);
    }
}
