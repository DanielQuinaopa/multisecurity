using multitrabajo_retiro.Models;

namespace multitrabajo_retiro.Services
{
    public interface IServiceTransaction
    {
        Task<Transaction> Withdrawal(Transaction transaction);
    }
}
