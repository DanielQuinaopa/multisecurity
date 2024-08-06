using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace multitrabajos_cuenta.Models
{
    public class Customer
    {
        [Key]
        //No permite genear identity en sql server
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int IdCustomer { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
