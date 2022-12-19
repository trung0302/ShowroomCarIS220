using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.Customer
{
    public class AddCustomerDTO
    {
        public string makh { get; set; }
        [Required]
        public string ten { get; set; }
        [Required]
        public string diachi { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string sodienthoai { get; set; }
        [Required]
        public string cccd { get; set; }
        [Required]
        public string password { get; set; }
    }
}
