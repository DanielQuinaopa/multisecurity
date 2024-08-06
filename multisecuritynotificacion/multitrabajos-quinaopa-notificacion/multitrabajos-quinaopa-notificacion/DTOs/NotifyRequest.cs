using System.ComponentModel.DataAnnotations.Schema;

namespace multitrabajos_quinaopa_notificacion.DTOs
{
    public class NotifyRequest
    {
        public int IdCuenta { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal Valor { get; set; }
    }
}
