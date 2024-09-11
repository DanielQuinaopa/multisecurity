namespace multitrabajos_cuenta.Services
{
    public interface IServiceAccount
    {
        Task<IEnumerable<Models.Account>> GetAll();
        Task<Models.Account> GetbyId(int id);
        Task<bool> Deposit(Models.Account account);
        Task<bool> Withdrawal(Models.Account account);
        Task<bool> NewAccount(Models.Customer customer);

    }
}
    