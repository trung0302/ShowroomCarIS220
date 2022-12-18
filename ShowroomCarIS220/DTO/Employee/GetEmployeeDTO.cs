using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.Employee
{
    public class GetEmployeeDTO
    {
        public Guid id { get; set; }
       
        public string manv { get; set; } = "NV0";
       
        public string name { get; set; }

      
        public string diachi { get; set; }

       
        public string ngaysinh { get; set; }

      
        public string chucvu { get; set; }

        
        public string gioitinh { get; set; }

     
        public string email { get; set; }

       
        public string sodienthoai { get; set; }

      
        public int cccd { get; set; }

       
        public DateTime createdAt { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
