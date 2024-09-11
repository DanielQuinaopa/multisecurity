namespace multisecurity.DTOs
{
    public class UserRequest
    {
        public int id { get; set; }

        public string? name { get; set; } 
        public string? lastName { get; set; } 
        public string? email { get; set; } 
        public string? password { get; set; } 
        public string? phone { get; set; } 
        public int RolId { get; set; }
    }
}
