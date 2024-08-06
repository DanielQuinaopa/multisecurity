using multitrabajo_retiro.DTOs;
using multitrabajo_retiro.Models;

namespace multitrabajo_retiro.Services
{
    public interface IServiceAccount
    {
        Task<bool> WithdrawAccount(AccountRequest request);
        bool Execute(Transaction request);
    }
}
