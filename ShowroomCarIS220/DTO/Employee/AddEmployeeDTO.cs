using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.Employee
{
    public class AddEmployeeDTO
    {
        public string manv { get; set; } = "NV0";

        [Required(ErrorMessage = "Please enter Name")]
        public string ten { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string diachi { get; set; }

        [Required(ErrorMessage = "Please enter DateOfBirth")]
        public DateTime ngaysinh { get; set; }

        [Required(ErrorMessage = "Please enter Position")]
        public string chucvu { get; set; }

        public string gioitinh { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        public string email { get; set; }


        [Required(ErrorMessage = "Please enter PhoneNumber")]
        public int sodienthoai { get; set; }


        [Required(ErrorMessage = "Please enter CCCD")]
        public int cccd { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "MinimumLength 8 charater")]
        public string password { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The password and confirmation password do not match")]
        public string confirmpassword { get; set; }
       
    }
}
