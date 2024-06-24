using multisecurity.DTOs;

namespace multisecurity.Services
{
    public interface IServiceLogin
    {
        Task<Models.User> Login(LoginRequest loginRequest);


    }
}
