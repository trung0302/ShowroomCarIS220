using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.User
{
    public class UpdateUser
    {
        public string? name { get; set; }
        public string? gioitinh { get; set; }
        public string? ngaysinh { get; set; }
        public string? sdt { get; set; }
        public string? diachi { get; set; }
        public string? chucvu { get; set; }
    }
}
