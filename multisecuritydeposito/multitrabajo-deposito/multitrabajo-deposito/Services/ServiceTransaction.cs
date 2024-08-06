using multitrabajo_deposito.Models;
using multitrabajo_deposito.Repositories;

namespace multitrabajo_deposito.Services
{
    public class ServiceTransaction : IServiceTransaction
    {
        private readonly ContextDatabase _contextDatabase;
        public ServiceTransaction(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }
        public async Task<Transaction> Deposit(Transaction transaction)
        {
            _contextDatabase.Transaction.Add(transaction);
            await _contextDatabase.SaveChangesAsync();
            return transaction;
        }
    }
}
