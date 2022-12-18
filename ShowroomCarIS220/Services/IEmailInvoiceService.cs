using ShowroomCarIS220.DTO.HoaDon;
using ShowroomCarIS220.Models;

namespace ShowroomCarIS220.Services
{
    public interface IEmailInvoiceService
    {
        public void SendInvoiceEmail(string emailTo, HoaDon hd, List<CTHD> cthds);
        public void SendOrderEmail(string emailTo, HoaDon hd, List<CTHD> cthds);
    }
}
