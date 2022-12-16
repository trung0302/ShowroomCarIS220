using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO
{
    public class AddCthdDTO
    {
        [Required]
        public string macar { get; set; }
        [Required]
        public int soluong { get; set; }
    }
}
