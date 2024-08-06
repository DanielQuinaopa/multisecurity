using multitrabajo_deposito.DTOs;

namespace multitrabajo_deposito.Services
{
    public interface IServiceNotification
    {
        Task<bool> DepositNotify(AccountRequest request);
        //bool Execute(Transaction request);
    }
}
