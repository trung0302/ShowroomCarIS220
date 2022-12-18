using ShowroomCarIS220.DTO.HoaDon;
using ShowroomCarIS220.Models;

namespace ShowroomCarIS220.Response
{
    public class InvoiceIdResponse
    {
        public GetInvoice hoadon { get; set; }
        public List<CTHD> cthds { get; set; }
    }
}
