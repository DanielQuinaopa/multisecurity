using Cordillera.Distribuidas.Event.Bus;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multisecurity.DTOs;
using multisecurity.Services;
using multitrabajo_retiro.Messages.Commands;

namespace multisecurity.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceUser _serviceUser;
        private readonly IEventBus _bus;
        public UserController(IServiceUser serviceUser, IEventBus bus)
        {
            _serviceUser = serviceUser;
            _bus = bus;
        }
        [HttpGet]
        public async Task<ActionResult> get()
        {
            var result = await _serviceUser.getAll();
            return Ok(new
            {
                result = result,
                message = result != null ? "OK" : "Not Content"

            });
        }
        [HttpGet("{id}")]
        //[Route("api/User/get2")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await _serviceUser.getUserbyId(id);
            return Ok(new
            {
                result = result,
                message = result != null ? "OK" : "Not Content"

            });
        }
        [HttpPost("save")]
        public async Task<ActionResult> save(UserRequest user)
        {
            Models.User userData = new Models.User
            {
                Id = user.id,
                Name = user.name,
                Email = user.email,
                LastName = user.lastName,
                Password = user.password,
                Phone = user.phone,
                RolId = user.RolId
            };
            var resultSave = await _serviceUser.save(userData);
            if (resultSave)
            {
                var UsuerCreateCommand = new UsuerCreateCommand
                (
                     user.id,
                     user.name,
                     user.email
                );
                await _bus.SendCommand(UsuerCreateCommand);
            }

            return Ok(new
            {
                result = resultSave,
                message = resultSave == true ? "Guardado Correctamente" : "Error al Guardar"
            }); ;
        }
    }
}
