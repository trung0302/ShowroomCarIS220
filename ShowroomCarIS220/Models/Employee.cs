using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.Models
{
    public class Employee
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string manv { get; set; } = "NV0";
        [Required]
        public string name { get; set; }

        [Required]
        public string diachi { get; set; }

        [Required]
        public string ngaysinh { get; set; }

        [Required]
        public string chucvu { get; set; }

        [Required]
        public string gioitinh { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string sodienthoai { get; set; }

        [Required]
        public int cccd { get; set; }
       
        [Required]
        public string password { get; set; }

        [Required]
        public string confirmpassword { get; set; }

        [Required]
        public DateTime createdAt { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
