using Microsoft.EntityFrameworkCore;
using multisecurity.DTOs;
using multisecurity.Models;
using multisecurity.Repositories;

namespace multisecurity.Services
{
    public class ServiceLogin : IServiceLogin
    {
        private readonly Contexto _contexto;
        private readonly IConfiguration _configuration;
        public ServiceLogin(Contexto contexto, IConfiguration configuration)
        { 
            _contexto = contexto;
            _configuration = configuration;
        }
        public async Task<User> Login(LoginRequest loginRequest)
        {
            return await authUser(loginRequest.Username, loginRequest.Password);
        }
        private async Task<User> authUser(string username, string password)
        {
            var userData = await _contexto.Usuario.Where(x => x.Estado.Equals("A") && x.Email.Equals(username) && x.Password.Equals(password)).FirstOrDefaultAsync();
            return userData;
        }
    }
}
