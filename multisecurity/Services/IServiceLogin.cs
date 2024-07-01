using multisecurity.DTOs;
using multisecurity.Models;

namespace multisecurity.Services
{
    public interface IServiceLogin
    {
        object generateToken(Models.User user);
        Task<Models.User> Login(LoginRequest loginRequest);

    }
}
