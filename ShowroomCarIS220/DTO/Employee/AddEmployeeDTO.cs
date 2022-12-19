using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.Employee
{
    public class AddEmployeeDTO
    {
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
        public string sdt { get; set; }

        [Required]
        public string password { get; set; }
    }
}
