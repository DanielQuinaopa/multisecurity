using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multisecurity.DTOs;
using multisecurity.Services;

namespace multisecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceUser _serviceUser;
        public UserController(IServiceUser serviceUser) 
        {
            _serviceUser = serviceUser;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            return Ok(new
            {
                result = resultSave,
                message = resultSave== true ? "Guardado Correctamente" : "Error al Guardar"
            }); ;
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> update(UserRequest user)
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
            var resultUpdate = await _serviceUser.update(userData);
            return Ok(new
            {
                result = resultUpdate,
                message = resultUpdate == true ? "Modificado Correctamente" : "Error al Modificar"
            }); ;
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var resultDelete = await _serviceUser.delete(id);
            return Ok(new
            {
                result = resultDelete,
                message = resultDelete == true ? "Eliminado Correctamente" : "Error al Eliminar"
            }); ;
        }
    }
}
