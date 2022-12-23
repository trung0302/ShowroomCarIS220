using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.Customer
{
    public class AddCustomerDTO
    {
        
        [Required]
        public string name { get; set; }
        [Required]
        public string diachi { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string sdt { get; set; }
        [Required]
        public string cccd { get; set; }
        [Required]
        public string ngaysinh { get; set; }
       
        public string gioitinh { get; set; }
        [Required]
        public string password { get; set; }
    }
}
