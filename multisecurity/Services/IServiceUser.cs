namespace multisecurity.Services
{
    public interface IServiceUser
    {
        Task<IEnumerable<Models.User>> getAll();
        Task<Models.User> getUserbyId(int id);
        Task<Models.User> getUserbyEmail(string email);
        Task<bool> save(Models.User user);
        Task<bool> update(Models.User user);
        Task<bool> delete(int id);
    }
}
