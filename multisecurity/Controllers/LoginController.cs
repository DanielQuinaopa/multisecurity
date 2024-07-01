using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multisecurity.Services;

namespace multisecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IServiceLogin _loginService;
        private readonly IServiceUser _serviceUser;

        public LoginController(IServiceLogin loginService, IServiceUser serviceUser)
        {
            _loginService = loginService;
            _serviceUser = serviceUser;
        }
        [HttpPost]
        [Route("/api/auth")]
        public async Task<ActionResult> Login(DTOs.LoginRequest login)
        {
            var user = await _loginService.Login(login);
            if (user != null)
            {
                return Ok(new
                {
                    token = _loginService.generateToken(user),
                    userId = user.Id,
                    userEmail = user.Email,
                    userName = user.Name
                });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
