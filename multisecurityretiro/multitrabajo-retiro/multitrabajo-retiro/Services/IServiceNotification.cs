using multitrabajo_retiro.DTOs;

namespace multitrabajo_retiro.Services
{
    public interface IServiceNotification
    {
        Task<bool> DepositNotify(AccountRequest request);
        //bool Execute(Transaction request);
    }
}
