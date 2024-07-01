using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using multisecurity.DTOs;
using multisecurity.Models;
using multisecurity.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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

        public object generateToken(Models.User user)
        {
            //Header
            var _symmetricSecurityKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(_configuration["JWT:Clave"])
               );
            var _sigingCredentials = new SigningCredentials(
                    _symmetricSecurityKey, SecurityAlgorithms.HmacSha256
                );

            var _Header = new JwtHeader(_sigingCredentials);

            //Claims - Publico
            var _claims = new[] {
             new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
             new Claim("nombre", user.Name),
             new Claim("apellido", user.LastName),
             new Claim(JwtRegisteredClaimNames.Email, user.Email)
     };

            //PayLoad
            var _Payload = new JwtPayload(
                    issuer: _configuration["JWT:Dominio"],
                    audience: _configuration["JWT:AppApi"],
                    claims: _claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(1)
                );

            //Token
            var _token = new JwtSecurityToken(_Header, _Payload);
            return new JwtSecurityTokenHandler().WriteToken(_token);

        }
    }
}
