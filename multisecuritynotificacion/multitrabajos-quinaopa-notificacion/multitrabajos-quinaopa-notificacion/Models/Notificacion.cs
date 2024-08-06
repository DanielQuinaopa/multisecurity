using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace multitrabajos_quinaopa_notificacion.Models
{
    public class Notificacion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("idcuenta")]
        public int IdCuenta { get; set; }
        [Column("tipo")]
        public string Tipo { get; set; } = null!;
        [Column("valor")]
        public decimal Valor { get; set; }

    }
}
