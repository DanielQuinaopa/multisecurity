namespace multitrabajo_deposito.DTOs
{
    public class NotifyRequest
    {
        public int Id { get; set; }
        public int IdCuenta { get; set; }
        public string Tipo { get; set; } = null!;
        public decimal Valor { get; set; }

    }
}
