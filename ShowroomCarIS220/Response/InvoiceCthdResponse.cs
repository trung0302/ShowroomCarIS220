using ShowroomCarIS220.DTO.HoaDon;

namespace ShowroomCarIS220.Response
{
    public class InvoiceCthdResponse
    {
        public GetInvoice hoadon { get; set; }
        public List<GetCthdDTO> cthds { get; set; }
    }
}
