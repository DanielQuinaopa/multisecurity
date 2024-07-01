using System.ComponentModel.DataAnnotations;

namespace multisecurity.Models
{
    public class User
    {
        [Key]//Primary Key
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? Phone { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateTime Add { get; set; }

        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }
    }
}
