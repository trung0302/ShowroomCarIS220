using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.Customer
{
    public class UpdateCustomerDTO
    {
        
        public string name { get; set; }
        public string ?diachi { get; set; }
        public string? gioitinh { get; set; }


        public string ?cccd { get; set; }
        public string ?sdt { get; set; }
        
        public string ?ngaysinh { get; set; }
    }
}
