using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowroomCarIS220.Models
{
    public class HoaDon
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string mahd { get; set; } = "HD0";
        [Required]
        public string makh { get; set; }
        [Required]
        public string tenkh { get; set; } = "KingSpeed";
        [Required]
        public string manv { get; set; }
        [Required]
        public string ngayhd { get; set; }
        [Required]
        public string tinhtrang { get; set; }
        [Required]
        public long trigia { get; set; } = 0;
        public List<CTHD> cthds { get; set; }
        [Required]
        public DateTime createdAt { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
