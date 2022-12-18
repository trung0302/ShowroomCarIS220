using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.Models
{
    public class Form
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string mobile { get; set; }
        [Required]
        public string email { get; set; }
        public string? message { get; set; }
        [Required]
        public DateTime createdAt { get; set; }   
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
