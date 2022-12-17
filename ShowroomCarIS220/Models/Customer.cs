using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.Models
{
    public class Customer
    {
        [Key]
        public Guid id { get; set; }
        [Required]
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
