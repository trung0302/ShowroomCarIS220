using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.HoaDon
{
    public class UpdateInvoiceDTO
    {
        [Required]
        public string tinhtrang { get; set; }
    }
}
