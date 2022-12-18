using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.Models
{
    public class User
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string mauser { get; set; } = "KH0";
        public string? gioitinh { get; set; }
        public string? ngaysinh { get; set; }
        public string? sdt { get; set; }
        public string? diachi { get; set; }
        public string? chucvu { get; set; }
        public string? cccd { get; set; }
        public string role { get; set; } = "customer";
        public string? verifyToken { get; set; }
        public List<Token>? token { get; set; }
        [Required]
        public DateTime createdAt { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
