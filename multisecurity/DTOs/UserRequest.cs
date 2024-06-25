namespace multisecurity.DTOs
{
    public class UserRequest
    {
        public int id { get; set; }

        public string name { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public string email { get; set; } = null!;
        public string password { get; set; } = null!;
        public string? phone { get; set; } = null!;
        public int RolId { get; set; }
    }
}
