using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using multitrabajos_quinaopa_notificacion.DTOs;
using multitrabajos_quinaopa_notificacion.Models;
using multitrabajos_quinaopa_notificacion.Services;

namespace multitrabajos_quinaopa_notificacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IServiceNotification _serviceNotification;

        public NotificationController(IServiceNotification serviceNotification)
        {
            _serviceNotification = serviceNotification;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _serviceNotification.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var notificacion = await _serviceNotification.GetById(id);
            if (notificacion == null)
            {
                return NotFound();
            }
            return Ok(notificacion);
        }

        //[HttpPost]
        //public async Task<ActionResult> Create(Notificacion notificacion)
        //{
        //    var result = await _serviceNotification.Create(notificacion);
        //    if (result)
        //    {
        //        return CreatedAtAction(nameof(GetById), new { id = notificacion.Id }, notificacion);
        //    }
        //    return BadRequest();
        //}
        [HttpPost("Notify")]
        public async Task<ActionResult> Notify(NotifyRequest request)
        {
            var result = await _serviceNotification.GetById(request.IdCuenta);
            Notificacion notification = new Notificacion()
            {
                IdCuenta = request.IdCuenta,
                Tipo = request.Tipo,
                Valor = request.Valor
            };
            await _serviceNotification.Create(notification);
            return Ok();
        }
    }
}
