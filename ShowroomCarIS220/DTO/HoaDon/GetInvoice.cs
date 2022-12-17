using ShowroomCarIS220.Models;
using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.HoaDon
{
    public class GetInvoice
    {
        public Guid id { get; set; }
        [Required]
        public string mahd { get; set; } = "HD0";
        [Required]
        public string manv { get; set; }
        [Required]
        public string makh { get; set; }
        [Required]
        public string tenkh { get; set; } = "KingSpeed";
        [Required]
        public string ngayhd { get; set; }
        [Required]
        public string tinhtrang { get; set; }
        [Required]
        public long trigia { get; set; } = 0;
        [Required]
        public DateTime createdAt { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
