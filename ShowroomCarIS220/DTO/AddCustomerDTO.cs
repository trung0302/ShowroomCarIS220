using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO
{
    public class AddCustomerDTO
    {
        public string makh { get; set; } = "KH0";
        [Required]
        public string ten { get; set; }
        [Required]
        public string diachi { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public long sodienthoai { get; set; }
        [Required]
        public long cccd { get; set; }
        [Required]
        public string password { get; set; }
    }
}
