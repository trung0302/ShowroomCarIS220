using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShowroomCarIS220.Models
{
    public class CTHD
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        [ForeignKey("HoaDon")]
        public string mahd { get; set; }
        [Required]
        public string macar { get; set; }
        [Required]
        public int soluong { get; set; } = 0;
        [Required]
        public long gia { get; set; } = 0;
        [Required]
        public DateTime createdAt { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
