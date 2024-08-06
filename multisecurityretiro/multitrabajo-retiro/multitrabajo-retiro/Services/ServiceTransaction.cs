using multitrabajo_retiro.Models;
using multitrabajo_retiro.Repositories;

namespace multitrabajo_retiro.Services
{
    public class ServiceTransaction : IServiceTransaction
    {
        private readonly ContextDatabase _contextDatabase;
        public ServiceTransaction(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }
        public async Task<Transaction> Withdrawal(Transaction transaction)
        {
            _contextDatabase.Transaction.Add(transaction);
            await _contextDatabase.SaveChangesAsync();
            return transaction;
        }
    }
}
