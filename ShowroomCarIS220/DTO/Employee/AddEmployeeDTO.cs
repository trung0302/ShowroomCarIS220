using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.Employee
{
    public class AddEmployeeDTO
    {
        [Required]
        public string name { get; set; }

        public string? diachi { get; set; }

        public string? ngaysinh { get; set; }
  
        public string? chucvu { get; set; }

        public string? cccd { get; set; }

        public string? gioitinh { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string? sdt { get; set; }

        [Required]
        public string password { get; set; }
    }
}
