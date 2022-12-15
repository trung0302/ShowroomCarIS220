using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.Models
{
    public class News
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string author { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string image { get; set; }
        [Required]
        public string description { get; set; }
        public string dateSource { get; set; }
        [Required]
        public string detail { get; set; }
        [Required]
        public DateTime createdAt { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
