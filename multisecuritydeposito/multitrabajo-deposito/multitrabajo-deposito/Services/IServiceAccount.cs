using multitrabajo_deposito.DTOs;
using multitrabajo_deposito.Models;

namespace multitrabajo_deposito.Services
{
    public interface IServiceAccount
    {
        Task<bool> DepositAccount(AccountRequest request);
        bool Execute(Transaction request);
    }
}
