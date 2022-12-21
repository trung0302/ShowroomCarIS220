using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.Employee
{
    public class GetEmployeeDTO
    {
        public Guid id { get; set; }
       
        public string mauser { get; set; } = "NV0";
       
        public string name { get; set; }

        public string? diachi { get; set; }

        public string? ngaysinh { get; set; }
      
        public string? chucvu { get; set; }
        public string? cccd { get; set; }
        public string? gioitinh { get; set; }

        public string email { get; set; }
        public string? sdt { get; set; }
        public string role { get; set; }
        public DateTime createdAt { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
