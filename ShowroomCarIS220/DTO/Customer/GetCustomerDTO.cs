using System.ComponentModel.DataAnnotations;

namespace ShowroomCarIS220.DTO.Customer
{
    public class GetCustomerDTO
    {
        public Guid id { get; set; }

        public string mauser { get; set; }

        public string ?name { get; set; }


        public string ?diachi { get; set; }


        public string ?ngaysinh { get; set; }

        public string ?gioitinh { get; set; }


        public string email { get; set; }


        public string ?sdt { get; set; }


        public string ?cccd { get; set; }


        public DateTime createdAt { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }
    }
}
