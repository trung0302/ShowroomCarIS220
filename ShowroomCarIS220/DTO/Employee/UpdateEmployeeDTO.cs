using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.Employee
{
    public class UpdateEmployeeDTO
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
        public string sodienthoai { get; set; }
        [Required]
        public int cccd { get; set; }
    }
}
