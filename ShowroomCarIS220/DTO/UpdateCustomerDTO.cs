using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO
{
    public class UpdateCustomerDTO
    {
        [Required]
        public string ten { get; set; }
        [Required]
        public string diachi { get; set; }
        [Required]
        public long cccd { get; set; }
        [Required]
        public long sodienthoai { get; set; }
    }
}
