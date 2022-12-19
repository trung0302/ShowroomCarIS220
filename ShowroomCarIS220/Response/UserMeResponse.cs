using ShowroomCarIS220.DTO.HoaDon;
using ShowroomCarIS220.DTO.User;
using ShowroomCarIS220.Models;

namespace ShowroomCarIS220.Response
{
    public class UserMeResponse
    {
        public GetUser user { get; set; }
        public List<GetInvoice>? hoadons { get; set; }
    }
}
