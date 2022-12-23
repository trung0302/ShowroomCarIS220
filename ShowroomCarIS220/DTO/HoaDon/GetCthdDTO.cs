using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.HoaDon
{
    public class GetCthdDTO
    {
        public Guid id { get; set; }
        public string macar { get; set; }
        [Required]
        public int soluong { get; set; }
        public string tenxe { get; set; }
        public long gia { get; set; }
        public string mahd { get; set; }
    }
}
