using ShowroomCarIS220.Models;
using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO
{
    public class AddInvoiceDTO
    {
        [Required]
        public string makh { get; set; }
        [Required]
        public string manv { get; set; }
        [Required]
        public string ngayhd { get; set; }
        [Required]
        public string tinhtrang { get; set; }
        [Required]
        public long trigia { get; set; } = 0;
    }
}
